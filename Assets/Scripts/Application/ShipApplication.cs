using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipApplication : BaseApplication<ShipModel, ShipView, ShipController>
{
    public override void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        controller.OnNotification(p_event_path, p_target, p_data);
    }
}
