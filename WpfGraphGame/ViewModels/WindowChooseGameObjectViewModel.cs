using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardGraphGame;

namespace WpfGraphGame.ViewModels
{
    public class WindowChooseGameObjectViewModel
    {

        #region propertyes

        /// <summary>
        /// Список объектов
        /// </summary>
        public List<GameObject> GameObjects
        {
            get;
            set;
        }

        public GameObject SelectedGameObject
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public WindowChooseGameObjectViewModel(List<GameObject> gameObjects)
        {
            GameObjects = new List<GameObject>(gameObjects);
        }

        #endregion

        #region methods

        #endregion

    }
}
