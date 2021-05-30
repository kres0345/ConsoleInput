﻿using static ConsoleInput.Input;

namespace ConsoleInput
{
    public static class Mouse
    {
        internal const int MOUSE_BUTTON_COUNT = 5;
        public readonly static bool[] MouseDown = new bool[MOUSE_BUTTON_COUNT];
        public readonly static bool[] MouseUp = new bool[MOUSE_BUTTON_COUNT];
        public readonly static bool[] MousePress = new bool[MOUSE_BUTTON_COUNT];
        public static short x { get; private set; } = 0;
        public static short y { get; private set; } = 0;

        private readonly static bool[] mouseDownPrevious = new bool[MOUSE_BUTTON_COUNT];
        private readonly static bool[] mouseDownCurrent = new bool[MOUSE_BUTTON_COUNT];

        internal static void Update()
        {
            // mousebutton held down
            for (int i = 0; i < MOUSE_BUTTON_COUNT; i++)
            {
                MouseUp[i] = false;
                MousePress[i] = false;


                if (mouseDownCurrent[i] != mouseDownPrevious[i])
                {
                    if (mouseDownCurrent[i])
                    {
                        MousePress[i] = true;
                        MouseDown[i] = true;
                    }
                    else
                    {
                        MouseUp[i] = true;
                        MouseDown[i] = false;
                    }
                }

                mouseDownPrevious[i] = mouseDownCurrent[i];
            }
        }

        internal static void HandleMouseEvent(MOUSE_EVENT_RECORD mouseEvent)
        {
            switch (mouseEvent.dwEventFlags)
            {
                case MouseEventFlags.DOUBLE_CLICK:
                    break;
                case MouseEventFlags.MOUSE_MOVED:
                    x = mouseEvent.dwMousePosition.X;
                    y = mouseEvent.dwMousePosition.Y;
                    break;
                case MouseEventFlags.MOUSE_HWHEELED:
                    break;
                case MouseEventFlags.MOUSE_WHEELED:
                    break;
                case 0:
                    // mousebutton pressed or up
                    for (int n = 0; n < MOUSE_BUTTON_COUNT; n++)
                    {
                        mouseDownCurrent[n] = (mouseEvent.dwButtonState & (1 << n)) != 0;
                    }
                    break;
            }
        }
    }
}