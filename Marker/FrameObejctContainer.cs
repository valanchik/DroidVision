﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YoloDetection.Marker
{
    public interface IFrameObejctContainer
    {
        List<IFrameObject> FrameObjectList { get; set; }
        IFrameObject Selected { get; set; }
        void Add(IFrameObject frameObejct);
        void Set(List<IFrameObject> list);
        void SetSelectedObject(int Id, bool seleted);
        void SetSelectedObject(IFrameObject d, bool seleted);
    }
    public class FrameObejctContainer: IFrameObejctContainer
    {
        public List<IFrameObject> FrameObjectList { get; set; } = new List<IFrameObject>();
        public IFrameObject Selected { get; set; }
        public void Set(List<IFrameObject> list)
        {
            FrameObjectList = list;
        }
        public void Add(IFrameObject frameObejct) {
            FrameObjectList.Add(frameObejct);
        }
        public void SetSelectedObject(int Id, bool seleted)
        {
            IFrameObject d = FrameObjectList.Find(fo => fo.Id == Id);
            SetSelectedObject(d, seleted);
        }
        public void SetSelectedObject(IFrameObject d, bool seleted)
        {
            if (d != null)
            {
                d.Selected = seleted;
                Selected = d;
                FrameObjectList.FindAll(fo =>
                {
                    if (fo.Id != d.Id)
                    {
                        fo.Selected = false;
                    };
                    return false;
                });
            }
            else
            {
                Selected = null;
            };
        }
    }
}
