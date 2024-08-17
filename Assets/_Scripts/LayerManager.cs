using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class LayerManager
    {
        #region Layers
        public static int PlayerLayer = 6;
        public static int BuildingLayer = 7;

        #endregion

        #region Layer Masks

        public static int PlayerLayerMask = 1 << PlayerLayer;
        public static int BuildingLayerMask = 1 << BuildingLayer;

        //public static int PlayerMask = DoorMask | WallMask;
        #endregion
        
        public static bool IsInLayerMask(int layerWantToCheck, LayerMask layerMask)
        {
            if (((1 << layerWantToCheck) & layerMask) != 0)
            {
                return true;
            }
            return false;
        }
    }

}
