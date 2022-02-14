using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RectTransform))]
    public class StatusManager : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] Transform parent;
        [SerializeField] StatusUI uiPrefab;
        [SerializeField] Camera cam;
        [SerializeField] Canvas uiCanvas;

        List<(StatusObservable, StatusUI)> observed = new List<(StatusObservable, StatusUI)>();
        GameObject player;
        RectTransform canvas;

        void Start()
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.canvas = this.uiCanvas.GetComponent<RectTransform>();
            foreach (var observable in GameObject.FindObjectsOfType<StatusObservable>())
            {
                var instance = Instantiate(this.uiPrefab, this.parent.position, Quaternion.identity);
                instance.transform.SetParent(this.parent);
                observed.Add((observable, instance));
            }
        }

        void Update()
        {
            for (var i = 0; i < this.observed.Count; i++)
            {
                var (observable, behaviour) = this.observed[i];
                if (observable == null)
                {
                    this.observed.RemoveAt(i);
                    behaviour.Remove();
                    continue;
                }

                var distance = Vector2.Distance(this.player.transform.position, observable.transform.position);
                var scale = (1 - (distance / observable.renderDistance)) * observable.statusSize;
                var observablePosition = new Vector3(
                    observable.transform.position.x + observable.xOffset,
                    observable.transform.position.y + observable.yOffset);
                var viewportPosition = cam.WorldToViewportPoint(observablePosition);
                var screenPosition = new Vector2(
                    ((viewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
                    ((viewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

                var isDungBall = observable.tag == "DungBall";
                if (isDungBall)
                {
                    var dungballRigidbody = observable.GetComponent<Rigidbody2D>();
                    behaviour.status.text = $"{observable.prefix}: {(int)dungballRigidbody.mass}";
                }
                else
                {
                    var weightTrigger = observable.GetComponent<WeightTrigger>();
                    behaviour.status.text = $"{observable.prefix}: {(int)weightTrigger.triggerMass}";
                }

                scale = scale <= 0f ? 0f : scale;
                behaviour.GetComponent<RectTransform>().anchoredPosition = screenPosition;
                behaviour.transform.localScale = new Vector3(scale, scale, 0f);
            }
        }
    }
}