using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace JHchoi.Models
{
    public class NoteModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        int noteMaxCount;
        float noteTerm = 1;

        public int NoteMaxCount { get => noteMaxCount; }
        public float NoteTerm { get => noteTerm; }

        public List<Dictionary<string, object>> GetNoteData(string dataName)
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> tempData;
            //tempData = CSVParse.CSVParser(Application.streamingAssetsPath +
            //                                        "/NoteData/" +
            //                                        dataName +
            //                                        "_NoteData.csv");

            //tempData = CSVParse.CSVParser("/NoteData/" + dataName + "_NoteData");
            tempData = CSVReader.Read("NoteData/"+dataName);


            data.AddRange(tempData);
            Debug.Log("data : " + data);

            noteMaxCount = data.Count;

            return data;
        }
    }
}