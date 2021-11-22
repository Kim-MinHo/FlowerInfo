using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace KMH
{
    public class ResourcesManger : SingletonMono<ResourcesManger>
    {
        public Sprite LoadSprite(string path)
        {
            byte[] byteTexture = System.IO.File.ReadAllBytes(path);

            if (byteTexture.Length > 0)
            {
                var texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                var rect = new Rect(0, 0, texture.width, texture.height);
                var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

                return sprite;
            }

            return null;
        }
    }
}


