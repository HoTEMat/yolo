using System;

namespace yolo {
    public static class PersonSpriteSelector {
        public static TimedSpriteSet GetWalkingSpriteSet(AssetBank assets, int personNum, PersonOrientation orientation) {
            var b = assets.TimedSprites;
            return personNum switch {
                1 => orientation switch {
                    PersonOrientation.Down => b.Person1Down,
                    PersonOrientation.Right => b.Person1Right,
                    PersonOrientation.Up => b.Person1Up,
                    PersonOrientation.Left => b.Person1Left,
                },
                2 => orientation switch {
                    PersonOrientation.Down => b.Person2Down,
                    PersonOrientation.Right => b.Person2Right,
                    PersonOrientation.Up => b.Person2Up,
                    PersonOrientation.Left => b.Person2Left,
                },
                3 => orientation switch {
                    PersonOrientation.Down => b.Person3Down,
                    PersonOrientation.Right => b.Person3Right,
                    PersonOrientation.Up => b.Person3Up,
                    PersonOrientation.Left => b.Person3Left,
                },
                4 => orientation switch {
                    PersonOrientation.Down => b.Person4Down,
                    PersonOrientation.Right => b.Person4Right,
                    PersonOrientation.Up => b.Person4Up,
                    PersonOrientation.Left => b.Person4Left,
                }
            };
        }

        public static Sprite GetStaticSprite(AssetBank assets, int personNum, PersonOrientation orientation) {
            var s = assets.Sprites;
            return personNum switch {
                1 => orientation switch {
                    PersonOrientation.Down => s.Person1Down,
                    PersonOrientation.Right => s.Person1Right,
                    PersonOrientation.Up => s.Person1Up,
                    PersonOrientation.Left => s.Person1Left,
                },
                2 => orientation switch {
                    PersonOrientation.Down => s.Person2Down,
                    PersonOrientation.Right => s.Person2Right,
                    PersonOrientation.Up => s.Person2Up,
                    PersonOrientation.Left => s.Person2Left,
                },
                3 => orientation switch {
                    PersonOrientation.Down => s.Person3Down,
                    PersonOrientation.Right => s.Person3Right,
                    PersonOrientation.Up => s.Person3Up,
                    PersonOrientation.Left => s.Person3Left,
                },
                4 => orientation switch {
                    PersonOrientation.Down => s.Person4Down,
                    PersonOrientation.Right => s.Person4Right,
                    PersonOrientation.Up => s.Person4Up,
                    PersonOrientation.Left => s.Person4Left,
                }
            };
        }
    }
}