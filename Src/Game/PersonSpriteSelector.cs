using System;

namespace yolo {
    public static class PersonSpriteSelector {
        public static TimedSpriteSet GetWalkingSpriteSet(AssetBank assets, int personNum, PlayerOrientation orientation) {
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
                },
                4 => orientation switch {
                    PlayerOrientation.Down => b.Person4Down,
                    PlayerOrientation.Right => b.Person4Right,
                    PlayerOrientation.Up => b.Person4Up,
                    PlayerOrientation.Left => b.Person4Left,
                }
            };
        }

        public static Sprite GetStaticSprite(AssetBank assets, int personNum, PlayerOrientation orientation) {
            var s = assets.Sprites;
            return personNum switch {
                1 => orientation switch {
                    PlayerOrientation.Down => s.Person1Down,
                    PlayerOrientation.Right => s.Person1Right,
                    PlayerOrientation.Up => s.Person1Up,
                    PlayerOrientation.Left => s.Person1Left,
                },
                2 => orientation switch {
                    PlayerOrientation.Down => s.Person2Down,
                    PlayerOrientation.Right => s.Person2Right,
                    PlayerOrientation.Up => s.Person2Up,
                    PlayerOrientation.Left => s.Person2Left,
                },
                3 => orientation switch {
                    PlayerOrientation.Down => s.Person3Down,
                    PlayerOrientation.Right => s.Person3Right,
                    PlayerOrientation.Up => s.Person3Up,
                    PlayerOrientation.Left => s.Person3Left,
                },
                4 => orientation switch {
                    PlayerOrientation.Down => s.Person4Down,
                    PlayerOrientation.Right => s.Person4Right,
                    PlayerOrientation.Up => s.Person4Up,
                    PlayerOrientation.Left => s.Person4Left,
                }
            };
        }
    }
}