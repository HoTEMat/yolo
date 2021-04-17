using System;

namespace yolo {
    public static class PersonSpriteSelector {
        public static TimedSpriteSet GetSpriteSet(AssetBank assets, int personNum, PlayerOrientation orientation) {
            var b = assets.TimedSprites;
            return personNum switch {
                1 => orientation switch {
                    PlayerOrientation.Down => b.Person1Down,
                    PlayerOrientation.Right => b.Person1Right,
                    PlayerOrientation.Up => b.Person1Up,
                    PlayerOrientation.Left => b.Person1Left,
                },
                2 => orientation switch {
                    PlayerOrientation.Down => b.Person2Down,
                    PlayerOrientation.Right => b.Person2Right,
                    PlayerOrientation.Up => b.Person2Up,
                    PlayerOrientation.Left => b.Person2Left,
                },
                3 => orientation switch {
                    PlayerOrientation.Down => b.Person3Down,
                    PlayerOrientation.Right => b.Person3Right,
                    PlayerOrientation.Up => b.Person3Up,
                    PlayerOrientation.Left => b.Person3Left,
                }
            };
        }
    }
}