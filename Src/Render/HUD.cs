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

        public void Draw()
        {
            if (!context.World.hud)
                return;
                
            var device = context.Graphics.GraphicsDevice;
            var viewport = context.Graphics.GraphicsDevice.Viewport;

            context.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            DrawClock(context.SpriteBatch, new Vector2(context.Graphics.GraphicsDevice.Viewport.Width - 50 - 250, 50), GetClockContent());
            DrawBucketList(context.SpriteBatch, new Vector2(50, 50));
            
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

        private void DrawClock(SpriteBatch spriteBatch, Vector2 corner, string content) {
            const int clockWidth = 250;
            const int clockHeight = 140;
            int offset = 5;
            
            spriteBatch.Draw(
                assets.Sprites.Empty.Texture,
                new Rectangle((int)(corner.X), (int)(corner.Y), clockWidth, clockHeight),
                assets.Sprites.Empty.SourceRect,
                Color.White);
            spriteBatch.Draw(
                assets.Sprites.Paper.Texture,
                new Rectangle((int)(corner.X) + offset, (int)(corner.Y) + offset, clockWidth - 2 * offset, clockHeight - 2 * offset),
                assets.Sprites.Paper.SourceRect,
                Color.White);
            spriteBatch.DrawStringCentered(content, corner - new Vector2(-clockWidth / 2, -clockHeight / 2), 8, Color.Black);
        }

        private void DrawBasket(SpriteBatch spriteBatch,  Viewport viewport)
        {
            Vector2 pos = new Vector2(50, viewport.Height - 80);
             spriteBatch.DrawSprite(assets.Sprites.Basket, pos, 3);
        }

        private void DrawBucketList(SpriteBatch spriteBatch, Vector2 position)
        {
            const int rowWidth = 14;
            const int rowHeight = 25;
            const int paddingLeft = 15;
            const int paddingTop = 15;
            int offset = 5;
            var list = context.Player.TodoList;

            int lm = 0;
            foreach (var listItem in list.Items)
            {
                string text = list.ItemText[listItem.Achievement] + (listItem.TotalCount > 1 ? " (" + listItem.DoneCount + "/" + listItem.TotalCount + ")" : "");
                lm = Math.Max(lm, text.Length);
            }
            
            spriteBatch.Draw(
                assets.Sprites.Empty.Texture,
                new Rectangle((int)position.X, (int)position.Y, rowWidth * lm + 2 * paddingTop, rowHeight * (list.Items.Count + 1) + 2 * paddingLeft),
                assets.Sprites.Empty.SourceRect,
                Color.White);
            
            spriteBatch.Draw(
                assets.Sprites.Paper.Texture,
                new Rectangle((int)position.X + offset, (int)position.Y + offset, rowWidth * lm + 2 * paddingTop - 2 * offset, rowHeight * (list.Items.Count + 1) + 2 * paddingLeft - 2 * offset),
                assets.Sprites.Paper.SourceRect,
                Color.White);
            
            spriteBatch.DrawStringCentered(list.Header, new Vector2(position.X + paddingLeft + rowWidth * lm / 2f, position.Y + paddingTop + 2), 2, Color.Black);
            
            spriteBatch.Draw(
                assets.Sprites.Empty.Texture,
                new Rectangle((int)(position.X), (int)(position.Y + paddingTop + 14), (int)(rowWidth * lm + 2 * paddingLeft), 3),
                assets.Sprites.Empty.SourceRect,
                Color.White);
            
            var curPos = new Vector2(position.X + paddingLeft, position.Y + paddingTop);
            foreach (var listItem in list.Items)
            {
                curPos = new Vector2(curPos.X, curPos.Y + rowHeight + 4);
                string text = list.ItemText[listItem.Achievement] + (listItem.TotalCount > 1 ? " (" + listItem.DoneCount + "/" + listItem.TotalCount + ")" : "");
                spriteBatch.DrawString(text, curPos, 2, Color.Black);
                if (listItem.AllDone)
                {
                    spriteBatch.Draw(assets.Sprites.Empty.Texture, 
                        new Rectangle((int)curPos.X, (int)curPos.Y + 6, rowWidth * text.Length, 3),
                        assets.Sprites.Empty.SourceRect, Color.Black);
                }
            }
        }
    }
}