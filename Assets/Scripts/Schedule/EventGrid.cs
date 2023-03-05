using System;
using UnityEngine;

public class EventGrid : MonoBehaviour {
    public EventConfiguration configuration;
    public GameObject prefab;
    public RectTransform gridTransform;
    public ScheduleController scheduleController;
    public EventConfirmationModal confirmationModal;

    private void Start() {
        PopulateGrid();
    }

    public void ResetGridItems() {
        var components = gridTransform.GetComponentsInChildren<EventGridItem>();
        Array.ForEach(components, item => item.Reset());
    }

    private void PopulateGrid() {
        configuration.presets.ForEach(CreateGridItem);
    }

    private void CreateGridItem(EventConfiguration.EventDefinition definition) {
        var obj = Instantiate(prefab, gridTransform);
        var component = obj.GetComponent<EventGridItem>();
        var logger = obj.AddComponent<ClickLogger>();
        logger.data = $"schedule_{definition.id}";
        if (component == null) {
            throw new Exception("Prefab component must have the PredefinedEventGridItem script attached!");
        }
        component.scheduleController = scheduleController;
        component.confirmationModal = confirmationModal;
        component.definition = definition;
        component.Initialize();
    }
}