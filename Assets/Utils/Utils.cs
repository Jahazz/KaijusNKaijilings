using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class Utils
    {
        public static string TryGetTypeByDirectory (string assetType, string assetName)
        {
            string[] guids = AssetDatabase.FindAssets($"t:{assetType} {assetName}");
            string output = string.Empty;

            if (guids.Length == 1)
            {
                output = new DirectoryInfo(Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(guids[0]))).Name;
            }
            else
            {
                Debug.LogWarning("Cannot automatically find Type");
            }

            return output;
        }

        public static List<ItemType> GetResourcesOfType<ItemType> ()
        {
            List<ItemType> output = new List<ItemType>();

            foreach (ItemType go in Resources.FindObjectsOfTypeAll(typeof(ItemType)) as ItemType[])
            {
                output.Add(go);
            }

            return output;
        }

        public static bool IsPositionOnCollider (Vector2 screenPosition, Collider collider)
        {
            bool output = false;
            var ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1 << collider.gameObject.layer))
            {
                if (raycastHit.collider == collider)
                {
                    output = true;
                }
            }

            return output;
        }

        public static bool AreTwoListsEqual<ListType> (IEnumerable<ListType> first, IEnumerable<ListType> second)
        {
            HashSet<ListType> firstHashset = new HashSet<ListType>(first);
            return firstHashset.SetEquals(second);
        }

        public void LookAtCameraBillboard (Transform targetTransform, Transform cameraTransform)
        {
            targetTransform.LookAt(targetTransform.position + cameraTransform.rotation * Vector3.back, cameraTransform.rotation * Vector3.up);
        }
    }
}

