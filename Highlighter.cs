using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIAutomationClient;

namespace WindowsFormsApp1
{
    public struct DrawItem
    {
        public DrawItem(bool _box, tagRECT _rect, Color _color)
        {
            box = _box;
            rect = _rect;
            color = _color;
        }
        public DrawItem(DrawItem clone)
        {
            box = clone.box;
            rect = clone.rect;
            color = clone.color;
        }
        public Color color;
        public bool box;
        public tagRECT rect;
    }

    public class Highlighter
    {
        static List<Highlighter> hightlights = new List<Highlighter>();
        ScreenBoundingRectangle _rectangle = new ScreenBoundingRectangle();

        int verticalPadding = 5;

        public Highlighter(DrawItem _item)
        {
            hightlights.Add(this);
            updateData(_item);
            
        }
        public void updateData(DrawItem _item)
        {
            _rectangle.Opacity = 0.5;
            _rectangle.Color = _item.color;
            
            if (_item.box)
            {
                _rectangle.Location = Rectangle.FromLTRB(_item.rect.left, _item.rect.top  , _item.rect.right, _item.rect.bottom);
            }
            else
            {
                _rectangle.Location = Rectangle.FromLTRB(_item.rect.left, _item.rect.top + verticalPadding , _item.rect.left, _item.rect.bottom - verticalPadding);
            }
        }
        public void DrawRectangle(DrawItem _item)
        {
            updateData(_item);
            this._rectangle.Visible = true;
        }

        public void Hide()
        {
            this._rectangle.Visible = false;
        }
        static public void BufferList(List<DrawItem> _buffer)
        {
            int i;
            for (i = 0; i < _buffer.Count; i++)
            {
                if (i >= hightlights.Count)
                {
                    new Highlighter(_buffer[i]);
                }
                else
                {
                    hightlights[i].DrawRectangle(_buffer[i]);
                }
            }

            for (int r = i; r < hightlights.Count; r++)
            {
                hightlights[r].Hide();
            }
        }
    }

    
}
