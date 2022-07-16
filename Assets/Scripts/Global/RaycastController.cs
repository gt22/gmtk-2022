using UnityEngine;

namespace Global
{
    public class RaycastController : MonoBehaviour
    {
        private RaycastHit2D _curRaycastHit;
        public bool debugRaycasts = false;

        private void HandleRaycastEvent(RaycastHit2D newHit)
        {
            // NO PREV HIT
            if (_curRaycastHit.collider == null)
            {
                if (newHit.collider != null)
                {
                    _curRaycastHit = newHit;
                    newHit.collider.gameObject.SendMessage("_OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                    if (debugRaycasts) Debug.LogWarning($"ON MOUSE ENTER [{newHit.collider.gameObject.name}]");
                }
            }
            //OLD HIT NOT NULL, there was something
            else
            {
                if (newHit.collider == null)
                {
                    _curRaycastHit.collider.gameObject.SendMessage("_OnMouseExit",
                        SendMessageOptions.DontRequireReceiver);
                    if (debugRaycasts) Debug.LogWarning($"ON MOUSE EXIT [{_curRaycastHit.collider.gameObject.name}]");
                    _curRaycastHit = new RaycastHit2D();
                    return;
                }

                // IT'S THE SAME
                if (newHit.collider.gameObject == _curRaycastHit.collider.gameObject)
                {
                    newHit.collider.gameObject.SendMessage("_OnMouseOver", SendMessageOptions.DontRequireReceiver);
                    if (debugRaycasts) Debug.LogWarning($"ON MOUSE OVER [{newHit.collider.gameObject.name}]");
                }
                // NEW OBJECT
                else
                {
                    _curRaycastHit.collider.gameObject.SendMessage("_OnMouseExit",
                        SendMessageOptions.DontRequireReceiver);
                    if (debugRaycasts) Debug.LogWarning($"ON MOUSE EXIT [{_curRaycastHit.collider.gameObject.name}]");
                    _curRaycastHit = newHit;
                    _curRaycastHit.collider.gameObject.SendMessage("_OnMouseEnter",
                        SendMessageOptions.DontRequireReceiver);
                    if (debugRaycasts) Debug.LogWarning($"ON MOUSE ENTER [{_curRaycastHit.collider.gameObject.name}]");
                }
            }
        }

        private void CheckRaycast()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            HandleRaycastEvent(hit);
        }

        private void FixedUpdate()
        {
            CheckRaycast();
            /*if (Input.GetKeyDown(KeyCode.Mouse0) && _curRaycastHit.collider != null)
            _curRaycastHit.collider.gameObject.SendMessage("_OnMouseClick", SendMessageOptions.DontRequireReceiver);*/
        }
    }
}