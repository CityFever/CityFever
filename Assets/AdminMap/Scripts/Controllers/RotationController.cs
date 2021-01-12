using UnityEngine;

namespace Assets.AdminMap.Scripts.Controllers
{
    public class RotationController : MonoBehaviour
    {
        public static void Rotate180Degrees(UnityObject unityObject)
        {
            StandardRotation(unityObject);

            /*switch (unityObject.Type())
            {
                case GameObjectType.Lamp:
                    LampSpecificRotation(unityObject);
                    break;
                case GameObjectType.House:
                    HouseSpecificRotation(unityObject);
                    break;
                default:
                    StandardRotation(unityObject);
                    break;
            }*/
        }

        private static void StandardRotation(UnityObject unityObject)
        {
            RotateTransform(unityObject.transform);
        }

        private static void HouseSpecificRotation(UnityObject unityObject)
        {
            
        }

        private static void TranslatePositionBy(Transform transform, int units)
        {
            Vector3 currentPosition = transform.position;
            transform.position = Vector3.Lerp(currentPosition, 
                new Vector3(currentPosition.x + units, currentPosition.y, currentPosition.z), Time.time);
        }

        private static void LampSpecificRotation(UnityObject unityObject)
        {
            Transform objectTransform = unityObject.transform;
            RotateTransform(objectTransform);
            Debug.Log(objectTransform.rotation.eulerAngles + " equals 180? " + objectTransform.rotation.eulerAngles.y.Equals(180.0f));
            int translationUnits = (int) objectTransform.rotation.eulerAngles.y == 180 ? -3 : 3;
            TranslatePositionBy(objectTransform, translationUnits);
        }

        private static void RotateTransform(Transform transform)
        {
            transform.rotation *= Quaternion.Euler(0, 180f, 0);
        }
    }
}
