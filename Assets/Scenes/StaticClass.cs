using UnityEngine;
static public class StaticClass
{
    public enum room
    {
        BEDROOM,
        HALL,
        VASHAL
    };
    public static room lastVisit = room.HALL;
}
