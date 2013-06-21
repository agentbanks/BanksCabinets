using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanksCabinets
{
    class Cabinet
    {
        string job, cabinetType, template, material;
        bool hasGlassDoors, isBoxDrawer, hasToeKick;
        int number, numGoodSides, adjustableShelves, fixedShelves, stainGradeShelves, rollouts;
        double goodSideThickness, width, height, depth, topRail, middleRail, bottomRail,
            rightStyle, leftStyle, middleStyle, middleShelfWidth, middleShelfHeight, middleShelfDepth;

        public Cabinet(string job, int number, string cabinetType, string template, string material, bool hasGlassDoors, bool isBoxDrawer, bool hasToeKick,
            int numGoodSides, int adjustableShelves, int fixedShelves, int stainGradeShelves, int rollouts, double goodSideThickness, double width, double height,
            double depth, double topRail, double middleRail, double bottomRail, double rightStyle, double leftStyle, double middleStyle, double middleShelfWidth,
            double middleShelfHeight, double middleShelfDepth)
        {
            this.job = job;
            this.number = number;
            this.cabinetType = cabinetType;
            this.template = template;
            this.material = material;
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

        public Cabinet(string job, int number)
        {
            this.job = job;
            this.number = number;
        }


    }
}
