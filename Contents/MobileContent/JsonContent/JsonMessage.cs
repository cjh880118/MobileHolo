using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.UI.Event
{
    public class CreateJosnFileMsg : Message
    {
        public string fileName;
        public string jsonFile;

        public CreateJosnFileMsg (string fileName, string jsonFile)
        {
            this.fileName = fileName;
            this.jsonFile = jsonFile;
        }
    }
}