using System;
using System.Collections.Generic;
using System.Text;
using StandardGraphGame;
using StandardEngineDraw;
using System.Linq;
using StandardGraphGameJSON;
using Newtonsoft.Json;
using System.Drawing;
using Plugin.Settings;

namespace StandardGraphGame
{
    enum GameSceneType
    {
        ChoiceLevel = 1,
        GameLevel = 2,
        GameEnd = 3
    }

    /// <summary>
    /// Класс игры
    /// </summary>
    public class GameGraph
    {
        #region fields

        /// <summary>
        /// Города
        /// </summary>
        List<City> _citys;

        /// <summary>
        /// Дороги
        /// </summary>
        List<GraphRoad> _roads;

        /// <summary>
        /// Транспорт
        /// </summary>
        List<Transport> _transports;

        List<LevelPoint> _levelPoints;

        /// <summary>
        /// Движок рисования
        /// </summary>
        IDrawEngine _drawEngine;

        /// <summary>
        /// Какой тип сцены у нас сейчас
        /// </summary>
        GameSceneType _sceneType;

        /// <summary>
        /// Вызов события загрузки уровня
        /// </summary>
        Action<int> _loadLevelAction;
        int _currentLevel;

        Button _nextLevelButton;
        Button _repeatCurrentLevelButton;
        Button _goChoiceLevelButton;
        bool _winLevel;

        #endregion

        #region propertyes
        #endregion

        #region constructors

        public GameGraph(IDrawEngine drawEngine, Action<int> loadLevel, bool generateDemoLevel = true)
        {
            _citys = new List<City>();
            _transports = new List<Transport>();
            _roads = new List<GraphRoad>();

            _levelPoints = new List<LevelPoint>();

            _drawEngine = drawEngine;

            CrossSettings.Current.AddOrUpdateValue("OpenLevel1", true);

            if (generateDemoLevel)
                GenerateDemoLevel();

            GenerateLevelPoints();
            _loadLevelAction = loadLevel;

            GenerateEndLevelButtons();

            _sceneType = GameSceneType.ChoiceLevel;

        }

        #endregion

        #region methods

        public void Click(float x, float y)
        {
            switch (_sceneType)
            {
                case GameSceneType.ChoiceLevel:
                    #region ChoiceLevel
                    foreach (LevelPoint levelPoint in _levelPoints)
                    {
                        if (levelPoint.CheckPoint(x, y))
                        {
                            if (levelPoint.IsData() && levelPoint.IsOpen())
                            {
                                _loadLevelAction(levelPoint.NumberLevel);
                                _currentLevel = levelPoint.NumberLevel;
                                return;
                            }
                            return;
                        }
                    }
                    #endregion
                    break;

                case GameSceneType.GameLevel:
                    #region GameLevel
                    //Проверяем грузовик
                    foreach (Transport transport in _transports)
                    {
                        if (transport.CheckPoint(x, y))
                        {
                            UnloadTransport(transport);
                            break;
                        }
                    }

                    //Проверка грузов
                    foreach (City city in _citys)
                    {
                        foreach (Cargo cargo in city.GetCargos())
                        {
                            if (cargo.City != null && cargo.CheckPoint(x, y))
                            {
                                LoadCargo(cargo);
                                break;
                            }
                        }
                    }

                    //Проверка дороги
                    foreach (GraphRoad road in _roads)
                    {
                        if (road.CheckPoint(x, y))
                        {
                            DriveRoad(road);
                            break;
                        }
                    }

                    if (CheckWin())
                        EndLevel(true);
                    else if (CheckLose())
                        EndLevel(false);

                    #endregion
                    break;

                case GameSceneType.GameEnd:
                    #region GameEnd

                    if (_winLevel && _nextLevelButton.CheckPoint(x, y))
                    {
                        _currentLevel++;
                        _loadLevelAction(_currentLevel);
                    }

                    if (_repeatCurrentLevelButton.CheckPoint(x, y))
                    {
                        _loadLevelAction(_currentLevel);
                    }

                    if (_goChoiceLevelButton.CheckPoint(x, y))
                    {
                        _sceneType = GameSceneType.ChoiceLevel;
                        ReDraw();
                    }

                    #endregion
                    break;
            }
        }

        /// <summary>
        /// Разгрузка транспорта
        /// </summary>
        /// <param name="transport"></param>
        public void UnloadTransport(Transport transport)
        {
            if (transport.Cargos.Count > 0)
            {
                transport.Cargos.ForEach(c => transport.City.AddCargo(c));
                transport.RemoveAllCargo();
                ReDraw(_drawEngine);
            }
        }

        /// <summary>
        /// Погрзука груза
        /// </summary>
        /// <param name="cargo"></param>
        public void LoadCargo(Cargo cargo)
        {
            //Ищем транспорт
            Transport transport = _transports.FirstOrDefault(t => t.City == cargo.City);

            if (transport != null)
            {
                if (transport.GetIsFree())
                {
                    cargo.City.RemoveCargo(cargo);
                    transport.AddCargo(cargo);
                    ReDraw(_drawEngine);
                }
            }
        }

        /// <summary>
        /// Едем по дороге
        /// </summary>
        /// <param name="road"></param>
        public void DriveRoad(GraphRoad road)
        {
            //Ищем транспорт
            Transport transport = _transports.FirstOrDefault(t => t.City == road.City1 || t.City == road.City2);

            if (transport != null)
            {
                transport.DriveRoad(road);
                ReDraw(_drawEngine);
            }
        }

        public void ReDraw(IDrawEngine engine = null)
        {
            if (engine == null)
                engine = _drawEngine;

            engine.Clear();

            engine.DrawImage(Properties.Resources.Fon, 0, 0, ConstantBase.X_LENGTH_GAME, ConstantBase.Y_LENGTH_GAME, 0, false);

            switch (_sceneType)
            {
                case GameSceneType.ChoiceLevel:
                    _levelPoints.ForEach(lp => lp.Draw(engine));
                    break;

                case GameSceneType.GameLevel:
                    _roads.ForEach(r => r.Draw(engine));
                    _citys.ForEach(c => c.Draw(engine));
                    _transports.ForEach(t => t.Draw(engine));
                    break;

                case GameSceneType.GameEnd:
                    if (_winLevel)
                    {
                        engine.DrawString(0, 0, "Вы выиграли!!!");
                        _nextLevelButton.Draw(engine);
                    }
                    else
                    {
                        engine.DrawString(0, 0, "Вы проиграли!!!");
                    }
                    _repeatCurrentLevelButton.Draw(engine);
                    _goChoiceLevelButton.Draw(engine);
                    break;
            }

        }

        /// <summary>
        /// Очистить игровые объекты уровня
        /// </summary>
        public void ClearLevel()
        {
            _citys.Clear();
            _transports.Clear();
            _roads.Clear();
        }

        /// <summary>
        /// Загрузить игровые объекты из входящей игры
        /// </summary>
        /// <param name="game"></param>
        public void LoadLevel(GameGraph game)
        {
            _citys.AddRange(game.GetCitys());
            _transports.AddRange(game.GetTransports());
            _roads.AddRange(game.GetRoads());
        }

        public void LoadLevel(int numLevel)
        {
            ClearLevel();

            object objectJson = Properties.Resources.ResourceManager.GetObject(string.Format("level{0}", numLevel));

            byte[] byteJson = (byte[])objectJson;

            string json = Encoding.ASCII.GetString(byteJson);

            GameGraphJSON gameJSON = JsonConvert.DeserializeObject<GameGraphJSON>(json);
            GameGraph game = JSON.Parsing.ParsingGameGraphJSONToGameGraph(gameJSON, _drawEngine);

            this.LoadLevel(game);
        }

        /// <summary>
        /// Завершаем уровень. Победой или нет
        /// </summary>
        /// <param name="win"></param>
        public void EndLevel(bool win)
        {
            _winLevel = win;
            _sceneType = GameSceneType.GameEnd;

            if (win)
                CrossSettings.Current.AddOrUpdateValue(string.Format("OpenLevel{0}", _currentLevel + 1), true);

            ReDraw();
        }

        public void StartLevel()
        {
            _sceneType = GameSceneType.GameLevel;
        }

        /// <summary>
        /// Создание демо-уровня для отладки работы игры
        /// </summary>
        private void GenerateDemoLevel()
        {
            //Три города
            _citys = new List<City>();

            City city1 = new City();
            city1.BitmapMain = ConstantBase.CARGO_TYPE_IMAGE[EnumCargo.apple];
            city1.XCoordinate = 100;
            city1.YCoordinate = 100;
            city1.AddCargoGoal(EnumCargo.apple, 1);

            City city2 = new City();
            city2.BitmapMain = ConstantBase.CARGO_TYPE_IMAGE[EnumCargo.orange];
            city2.XCoordinate = 400;
            city2.YCoordinate = 400;
            city2.AddCargoGoal(EnumCargo.orange, 1);

            City city3 = new City();
            city3.BitmapMain = ConstantBase.CARGO_TYPE_IMAGE[EnumCargo.peach];
            city3.XCoordinate = 700;
            city3.YCoordinate = 700;
            city3.AddCargoGoal(EnumCargo.peach, 1);

            _citys.Add(city1);
            _citys.Add(city2);
            _citys.Add(city3);
            ///

            Cargo cargo1 = new Cargo();
            cargo1.TypeCargo = EnumCargo.apple;
            cargo1.City = city3;
            city3.AddCargo(cargo1);

            Cargo cargo2 = new Cargo();
            cargo2.TypeCargo = EnumCargo.orange;
            cargo2.City = city1;
            city1.AddCargo(cargo2);

            Cargo cargo3 = new Cargo();
            cargo3.TypeCargo = EnumCargo.peach;
            cargo3.City = city2;
            city2.AddCargo(cargo3);
            ///

            ///Транспорт
            _transports = new List<Transport>();

            Transport transport = new Transport();
            transport.BitmapMain = Properties.Resources.Trunk;
            transport.City = city1;

            _transports.Add(transport);
            ///

            ///Дороги
            _roads = new List<GraphRoad>();

            GraphRoad road12 = new GraphRoad();
            road12.City1 = city1;
            road12.City2 = city2;
            road12.CountPermittedPasses = 2;

            GraphRoad road23 = new GraphRoad();
            road23.City1 = city2;
            road23.City2 = city3;
            road23.CountPermittedPasses = 2;

            _roads.Add(road12);
            _roads.Add(road23);
            ///


        }

        private void GenerateLevelPoints()
        {
            _levelPoints = new List<LevelPoint>();

            int num = 1;

            int j = 0;

            int xLengthLevel = ConstantBase.X_LENGTH_GAME_BASE / ConstantBase.X_COUNT_LEVEL_ROW;
            xLengthLevel = (int)((float)xLengthLevel * 0.9f);

            while (num <= ConstantBase.X_COUNT_LEVEL)
            {
                for (int i = 0; i < ConstantBase.X_COUNT_LEVEL_ROW; i++)
                {
                    if (num > ConstantBase.X_COUNT_LEVEL)
                        break;

                    LevelPoint levelPoint = new LevelPoint();
                    levelPoint.NumberLevel = num;
                    levelPoint.XLength = xLengthLevel;
                    levelPoint.YLength = xLengthLevel;
                    levelPoint.XCoordinate = i * xLengthLevel;
                    levelPoint.YCoordinate = j * xLengthLevel;

                    _levelPoints.Add(levelPoint);

                    num++;
                }

                j++;
            }
        }

        /// <summary>
        /// Создать объекты для меню окончания уровня
        /// </summary>
        private void GenerateEndLevelButtons()
        {
            _nextLevelButton = new Button();
            _nextLevelButton.XCoordinate = 0;
            _nextLevelButton.XLength = ConstantBase.X_LENGTH_END_LEVEL_BUTTON;
            _nextLevelButton.YCoordinate = ConstantBase.Y_START_BUTTONS_END;
            _nextLevelButton.YLength = ConstantBase.Y_LENGTH_END_LEVEL_BUTTON;
            _nextLevelButton.Caption = "Следующий уровень";
            _nextLevelButton.BitmapMain = Properties.Resources.Level;

            _repeatCurrentLevelButton = new Button();
            _repeatCurrentLevelButton.XCoordinate = 0;
            _repeatCurrentLevelButton.XLength = ConstantBase.X_LENGTH_END_LEVEL_BUTTON;
            _repeatCurrentLevelButton.YCoordinate = ConstantBase.Y_LENGTH_END_LEVEL_BUTTON +
                ConstantBase.Y_LENGTH_END_LEVEL_BUTTON +
                ConstantBase.Y_INTERVAL_BEETWEEN_END_LEVEL_BUTTONS;
            _repeatCurrentLevelButton.YLength = ConstantBase.Y_LENGTH_END_LEVEL_BUTTON;
            _repeatCurrentLevelButton.Caption = "Повторить";
            _repeatCurrentLevelButton.BitmapMain = Properties.Resources.Level;

            _goChoiceLevelButton = new Button();
            _goChoiceLevelButton.XCoordinate = 0;
            _goChoiceLevelButton.XLength = ConstantBase.X_LENGTH_END_LEVEL_BUTTON;
            _goChoiceLevelButton.YCoordinate = ConstantBase.Y_START_BUTTONS_END +
                2 * (ConstantBase.Y_LENGTH_END_LEVEL_BUTTON +
                ConstantBase.Y_INTERVAL_BEETWEEN_END_LEVEL_BUTTONS);
            _goChoiceLevelButton.YLength = ConstantBase.Y_LENGTH_END_LEVEL_BUTTON;
            _goChoiceLevelButton.Caption = "Выбрать уровень";
            _goChoiceLevelButton.BitmapMain = Properties.Resources.Level;
        }

        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        /// <returns></returns>
        private bool CheckWin()
        {
            foreach (City city in _citys)
            {
                if (!city.CheckWin())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Уровень проигран
        /// </summary>
        /// <returns></returns>
        private bool CheckLose()
        {
            City city = _transports.First().City;

            //Уровень не проигран если еще есть дороги
            if (_roads.Any(r => (r.City1 == city || r.City2 == city) && r.CountPermittedPasses > 0))
                return false;

            if (_transports.First().Cargos.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Добавить город
        /// </summary>
        /// <param name="city"></param>
        public void AddCity(City city)
        {
            _citys.Add(city);
        }

        public List<City> GetCitys()
        {
            return new List<City>(_citys);
        }

        public void AddRoad(GraphRoad road)
        {
            _roads.Add(road);
        }

        public List<GraphRoad> GetRoads()
        {
            return new List<GraphRoad>(_roads);
        }

        public void DeleteRoad(GraphRoad road)
        {
            _roads.Remove(road);
        }

        public void DeleteCity(City city)
        {
            _roads.RemoveAll(r => r.City1 == city || r.City2 == city);
            _citys.Remove(city);

            List<Transport> transports = _transports.FindAll(t => t.City == city);

            if (_citys.Count > 0)
            {
                transports.ForEach(t => t.City = _citys.First());
            }
        }

        public List<Transport> GetTransports()
        {
            return new List<Transport>(_transports);
        }

        public void AddTransport(Transport transport)
        {
            _transports.Add(transport);
        }

        public City GetCityById(int id)
        {
            return _citys.FirstOrDefault(c => c.Id == id);
        }

        #endregion
    }
}
