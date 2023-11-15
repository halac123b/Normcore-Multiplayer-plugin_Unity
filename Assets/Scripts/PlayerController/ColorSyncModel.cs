using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Realtime model chứa data Color
[RealtimeModel]
public partial class ColorSyncModel
{
    /* 3 arg: PropertyID
        Reliable: nếu prop thay đổi nhiều (animation) thì để false, khi đó server không cần resend nếu lúc gửi bị lỗi, ngược lại prop thay đổi ít cần sự chính xác cao thì rely.
        Change event: nếu true, sẽ thêm 1 Event để thông báo khi gt của prop thay đổi
    */
    [RealtimeProperty(1, true, true)] private Color _color;
}
