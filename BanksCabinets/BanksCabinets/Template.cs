using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanksCabinets
{
    class Template
    {
        public string cabinetType, template;
        public bool hasGlassDoors, isBoxDrawer, hasToeKick;
        public int numGoodSides, adjustableShelves, fixedShelves, stainGradeShelves, rollouts, numOpenings;
        public double goodSideThickness, width, height, depth, topRail, middleRail1, middleRail2, middleRail3, middleRail4, bottomRail,
            rightStyle, leftStyle, middleStyle, middleShelfWidth, middleShelfHeight, middleShelfDepth;
       // public double[] middleRails = new double[4];


         public Template(string cabinetType, string template, bool hasGlassDoors, bool isBoxDrawer, bool hasToeKick,
            int numGoodSides, int adjustableShelves, int fixedShelves, int stainGradeShelves, int rollouts, int numOpenings, double goodSideThickness, double width, double height,
            double depth, double topRail, double middleRail1, double middleRail2, double middleRail3, double middleRail4, double bottomRail, double rightStyle, double leftStyle, double middleStyle, double middleShelfWidth,
            double middleShelfHeight, double middleShelfDepth)
        {            
            this.cabinetType = cabinetType;
            this.template = template;            
            this.hasGlassDoors = hasGlassDoors;
            this.isBoxDrawer = isBoxDrawer;
            this.hasToeKick = hasToeKick;
            this.numGoodSides = numGoodSides;
            this.adjustableShelves = adjustableShelves;
            this.fixedShelves = fixedShelves;
            this.stainGradeShelves = stainGradeShelves;
            this.rollouts = rollouts;
            this.goodSideThickness = goodSideThickness;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.numOpenings = numOpenings;
            this.topRail = topRail;
            this.middleRail1 = middleRail1;
            this.middleRail2 = middleRail2;
            this.middleRail3 = middleRail3;
            this.middleRail4 = middleRail4;
            this.bottomRail = bottomRail;
            this.rightStyle = rightStyle;
            this.leftStyle = leftStyle;
            this.middleStyle = middleStyle;
            this.middleShelfWidth = middleShelfWidth;
            this.middleShelfHeight = middleShelfHeight;
            this.middleShelfDepth = middleShelfDepth;
        }

         public override string ToString()
         {
             return template;
         }
    }
}
