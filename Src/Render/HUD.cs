using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class HUD {
        private Context context;
        private AssetBank assets => context.Assets;

        //private BucketList bucketList = new BucketList(new List<BucketListItem>());
        public HUD(Context context) {
            this.context = context;
            //bucketList.FillBucketList(true);
        }

        public void Draw() {
            var device = context.Graphics.GraphicsDevice;
            var camera = context.Camera;
            var viewport = context.Graphics.GraphicsDevice.Viewport;

            context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            DrawClock(context.SpriteBatch, new Vector2(150, 90), GetClockContent());
            DrawBucketList(context.SpriteBatch, new Vector2(25, 200));
            if (context.Player.HasGroceries)
            {
                DrawBasket(context.SpriteBatch, viewport);
            }
            
            context.SpriteBatch.End();
        }

        private string GetClockContent() {
            World w = context.World;
            if (w == null)
                return "---";
            int ttl = (int) w.TimeToLive;
            int seconds = ttl % 60;
            return seconds + "";
        }

        private void DrawClock(SpriteBatch spriteBatch, Vector2 center, string content) {
            const int clockWidth = 250;
            const int clockHeight = 150;
            Vector2 toUpperLeft = new Vector2(-clockWidth / 2, -clockHeight / 2);
            spriteBatch.Draw(
                assets.Sprites.Empty.Texture,
                new Rectangle((int)(center.X + toUpperLeft.X), (int)(center.Y + toUpperLeft.Y), clockWidth, clockHeight),
                assets.Sprites.Empty.SourceRect,
                Color.White);
            DrawStringCentered(spriteBatch, content, center, 8, Color.White);
        }

        private void DrawBasket(SpriteBatch spriteBatch,  Viewport viewport)
        {
            Vector2 pos = new Vector2(10, viewport.Height - 60);
             spriteBatch.DrawSprite(assets.Sprites.Basket, pos, 3);
        }

        private void DrawBucketList( SpriteBatch spriteBatch, Vector2 position)
        {
            const int rowWidth = 14;
            const int rowHeight = 25;
            const int paddingLeft = 15;
            const int paddingTop = 15;
            var list = context.Player.TodoList;

            int lm = 0;
            foreach (var listItem in list.Items)
            {
                string text = list.ItemText[listItem.Achievement] + (listItem.TotalCount > 1 ? " (" + listItem.DoneCount + "/" + listItem.TotalCount + ")" : "");
                lm = Math.Max(lm, text.Length);
            }
            
            // pozadí
            spriteBatch.Draw(
                assets.Sprites.Paper.Texture,
                new Rectangle((int)position.X, (int)position.Y, rowWidth * lm + 2 * paddingTop, rowHeight * (list.Items.Count + 1) + 2 * paddingLeft),
                assets.Sprites.Paper.SourceRect,
                Color.White);
            
            DrawStringCentered(spriteBatch, list.Header, new Vector2(position.X + paddingLeft + rowWidth * lm / 2f, position.Y + paddingTop + 2), 2, Color.Black);
            
            var curPos = new Vector2(position.X + paddingLeft, position.Y + paddingTop);
            foreach (var listItem in list.Items)
            {
                curPos = new Vector2(curPos.X, curPos.Y + rowHeight);
                string text = list.ItemText[listItem.Achievement] + (listItem.TotalCount > 1 ? " (" + listItem.DoneCount + "/" + listItem.TotalCount + ")" : "");
                DrawString(spriteBatch, text, curPos, 2, Color.Black);
                if (listItem.AllDone)
                {
                    spriteBatch.Draw(assets.Sprites.Empty.Texture, 
                        new Rectangle((int)curPos.X, (int)curPos.Y + 6, rowWidth * text.Length, 3),
                        assets.Sprites.Empty.SourceRect, Color.Black);
                }
            }
        }

        private void DrawString(SpriteBatch spriteBatch, string s, Vector2 position, float scale, Color fontColor) {
            spriteBatch.DrawString(assets.Fonts.Font, s, position, fontColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private void DrawStringCentered(SpriteBatch spriteBatch, string s, Vector2 center, float scale, Color fontColor) {
            float charW = assets.Fonts.Font.Glyphs[0].Width;
            float strW = s.Length * charW;
            float strH = charW;
            
            DrawString(spriteBatch, s, center - new Vector2(strW / 2, strH / 2) * scale, scale, fontColor);
        }
    }
}