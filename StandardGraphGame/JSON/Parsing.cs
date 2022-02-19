using System;
using System.Collections.Generic;
using System.Text;
using StandardGraphGameJSON;
using StandardEngineDraw;
using System.ComponentModel;

namespace StandardGraphGame.JSON
{
    public static class Parsing
    {
        #region GameGraph->JSON

        public static GameGraphJSON ParsingGameGraphToGameGraphJSON(GameGraph game)
        {
            GameGraphJSON gameJSON = new GameGraphJSON();

            //Citys
            foreach (City city in game.GetCitys())
            {
                CityJSON cityJSON = ParsingCityToCityJSON(game, city);
                gameJSON.Citys.Add(cityJSON);
            }
            //---

            //Roads
            foreach (GraphRoad road in game.GetRoads())
            {
                RoadJSON roadJSON = ParsingRoadToRoadJSON(game, road);
                gameJSON.Roads.Add(roadJSON);
            }
            //---

            //Transports
            foreach (Transport transport in game.GetTransports())
            {
                TransportJSON transportJSON = ParsingTransportToTransportJSON(game, transport);
                gameJSON.Transports.Add(transportJSON);
            }
            //---

            return gameJSON;
        }

        private static CityJSON ParsingCityToCityJSON(GameGraph game, City city)
        {
            CityJSON cityJSON = new CityJSON();

            CopyParametersFromGameObjectToGameObjectJSON(city, cityJSON);

            foreach (Cargo cargo in city.GetCargos())
            {
                CargoJSON cargoJSON = ParsingCargoToCargoJSON(game, cargo);
                cityJSON.Cargos.Add(cargoJSON);
            }

            foreach (EnumCargo type in city.GetCargosGoal().Keys)
            {
                cityJSON.CagrosGoalJSON.Add((int)type, city.GetCargosGoalCount(type));
            }

            return cityJSON;
        }

        private static RoadJSON ParsingRoadToRoadJSON(GameGraph game, GraphRoad road)
        {
            RoadJSON roadJSON = new RoadJSON();

            CopyParametersFromGameObjectToGameObjectJSON(road, roadJSON);

            roadJSON.City1Id = road.City1.Id;
            roadJSON.City2Id = road.City2.Id;

            roadJSON.CountPermittedPasses = road.CountPermittedPasses;

            return roadJSON;
        }

        private static TransportJSON ParsingTransportToTransportJSON(GameGraph game, Transport transport)
        {
            TransportJSON transportJSON = new TransportJSON();

            CopyParametersFromGameObjectToGameObjectJSON(transport, transportJSON);

            transportJSON.Capacity = transport.Capacity;
            transportJSON.CityId = transport.City.Id;

            foreach (Cargo cargo in transport.Cargos)
            {
                CargoJSON cargoJSON = ParsingCargoToCargoJSON(game, cargo);
                transportJSON.Cargos.Add(cargoJSON);
            }

            return transportJSON;
        }

        private static CargoJSON ParsingCargoToCargoJSON(GameGraph game, Cargo cargo)
        {
            CargoJSON cargoJSON = new CargoJSON();

            CopyParametersFromGameObjectToGameObjectJSON(cargo, cargoJSON);

            cargoJSON.TransportId = cargo.Transport?.Id;
            cargoJSON.CityId = cargo.City?.Id;

            cargoJSON.TypeCargo = (int)cargo.TypeCargo;

            return cargoJSON;
        }

        private static void CopyParametersFromGameObjectToGameObjectJSON(GameObject from, GameObjectJSON to)
        {
            to.Id = from.Id;
            to.XCoordinate = from.XCoordinate;
            to.YCoordinate = from.YCoordinate;
            to.XLength = from.XLength;
            to.YLength = from.YLength;
        }

        #endregion

        #region JSON->GameGraph

        public static GameGraph ParsingGameGraphJSONToGameGraph(GameGraphJSON gameJSON, IDrawEngine drawEngine)
        {
            GameGraph game = new GameGraph(drawEngine, null, false);

            //Citys
            foreach (CityJSON cityJSON in gameJSON.Citys)
            {
                City city = ParsingCityJSONToCity(cityJSON);
                game.AddCity(city);
            }
            //---

            //Roads
            foreach (RoadJSON roadJSON in gameJSON.Roads)
            {
                GraphRoad road = ParsingRoadJSONToRoad(game, roadJSON);
                game.AddRoad(road);
            }
            //---

            //Transports
            foreach (TransportJSON transportJSON in gameJSON.Transports)
            {
                Transport transport = ParsingTransportJSONToTransport(game, transportJSON);
                game.AddTransport(transport);
            }
            //---

            return game;
        }

        private static City ParsingCityJSONToCity(CityJSON cityJSON)
        {
            City city = new City();

            CopyParametersFromGameObjectJSONToGameObject(cityJSON, city);

            foreach (CargoJSON cargoJSON in cityJSON.Cargos)
            {
                Cargo cargo = ParsingCargoJSONToCargo(cargoJSON, city, null);
                city.AddCargo(cargo);
            }

            foreach (int typeCargoId in cityJSON.CagrosGoalJSON.Keys)
            {
                city.AddCargoGoal((EnumCargo)typeCargoId, cityJSON.CagrosGoalJSON[typeCargoId]);
            }

            return city;


        }

        public static GraphRoad ParsingRoadJSONToRoad(GameGraph game, RoadJSON roadJSON)
        {
            GraphRoad road = new GraphRoad();

            CopyParametersFromGameObjectJSONToGameObject(roadJSON, road);

            road.City1 = game.GetCityById(roadJSON.City1Id);
            road.City2 = game.GetCityById(roadJSON.City2Id);

            road.CountPermittedPasses = roadJSON.CountPermittedPasses;

            return road;
        }

        public static Transport ParsingTransportJSONToTransport(GameGraph game, TransportJSON transportJSON)
        {
            Transport transport = new Transport();

            CopyParametersFromGameObjectJSONToGameObject(transportJSON, transport);

            transport.Capacity = transportJSON.Capacity;

            foreach (CargoJSON cargoJSON in transportJSON.Cargos)
            {
                Cargo cargo = ParsingCargoJSONToCargo(cargoJSON, null, transport);
                transport.Cargos.Add(cargo);
            }

            transport.City = game.GetCityById(transportJSON.CityId);

            transport.XLength = ConstantBase.X_LENGTH_TRANSPORT;
            transport.YLength = ConstantBase.Y_LENGTH_TRANSPORT;

            return transport;
        }

        private static Cargo ParsingCargoJSONToCargo(CargoJSON cargoJSON, City city, Transport transport)
        {
            Cargo cargo = new Cargo((EnumCargo)cargoJSON.TypeCargo);

            CopyParametersFromGameObjectJSONToGameObject(cargoJSON, cargo);

            cargo.Transport = transport;
            cargo.City = city;

            cargo.XLength = ConstantBase.X_CARGO_LENGTH;
            cargo.YLength = ConstantBase.Y_CARGO_LENGTH;

            return cargo;
        }

        private static void CopyParametersFromGameObjectJSONToGameObject(GameObjectJSON from, GameObject to)
        {
            to.Id = from.Id;
            to.XCoordinate = from.XCoordinate;
            to.YCoordinate = from.YCoordinate;
            to.XLength = from.XLength;
            to.YLength = from.YLength;
        }

        #endregion
    }
}
