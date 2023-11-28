﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Xml;

namespace motionRecovery
{
    // Class responsible for reading exercise data from an XML file
    internal class ExercisesReaderXML
    {

        private ParseJointType ParseJointType; // Used to parse a string to a type Joint

        // Used to read exercise data from an XML file and return a list of positions
        public List<Position> ReadExerciseFile(string filePath)
        {
            List<Position> positionList = new List<Position>();
            ParseJointType parseJointType = new ParseJointType();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                XmlNodeList positionNodes = xmlDoc.SelectNodes("//Position");

                foreach (XmlNode positionNode in positionNodes)
                {
                    Position myPosition = new Position();

                    myPosition.Joint1 = parseJointType.ParseToJoint(positionNode.SelectSingleNode("Membre1").InnerText.Trim());
                    myPosition.Joint2 = parseJointType.ParseToJoint(positionNode.SelectSingleNode("Membre2").InnerText.Trim());
                    myPosition.AngleMin = Convert.ToDouble(positionNode.SelectSingleNode("AngleMin").InnerText.Trim());
                    myPosition.AngleMax = Convert.ToDouble(positionNode.SelectSingleNode("AngleMax").InnerText.Trim());
                    myPosition.PositionTime = Convert.ToDouble(positionNode.SelectSingleNode("PositionTime").InnerText.Trim());
                    myPosition.Description = positionNode.SelectSingleNode("Description").InnerText.Trim();

                    positionList.Add(myPosition);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the exercise file: {ex.Message}");
            }

            return positionList;
        }
    }
}
