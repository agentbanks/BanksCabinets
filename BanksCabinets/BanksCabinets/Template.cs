using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanksCabinets
{
    class Template
    {
        string cabinetType, template;
        bool hasGlassDoors, isBoxDrawer, hasToeKick;
        int numGoodSides, adjustableShelves, fixedShelves, stainGradeShelves, rollouts;
        double goodSideThickness, width, height, depth, topRail, middleRail, bottomRail,
            rightStyle, leftStyle, middleStyle, middleShelfWidth, middleShelfHeight, middleShelfDepth;


         public Template(string cabinetType, string template, bool hasGlassDoors, bool isBoxDrawer, bool hasToeKick,
            int numGoodSides, int adjustableShelves, int fixedShelves, int stainGradeShelves, int rollouts, double goodSideThickness, double width, double height,
            double depth, double topRail, double middleRail, double bottomRail, double rightStyle, double leftStyle, double middleStyle, double middleShelfWidth,
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
            this.topRail = topRail;
            this.middleRail = middleRail;
            this.bottomRail = bottomRail;
            this.rightStyle = rightStyle;
            this.leftStyle = leftStyle;
            this.middleStyle = middleStyle;
            this.middleShelfWidth = middleShelfWidth;
            this.middleShelfHeight = middleShelfHeight;
            this.middleShelfDepth = middleShelfDepth;
        }
    }
}
