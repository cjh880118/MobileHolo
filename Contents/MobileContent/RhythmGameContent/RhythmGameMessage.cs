using CellBig.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.UI.Event
{
    public class RhythmGameMusicListMsg : Message
    {
        public List<BT_Sound.Param> ListRhythmGameMusic;
        public RhythmGameMusicListMsg(List<BT_Sound.Param> ListRhythmGameMusic)
        {
            this.ListRhythmGameMusic = ListRhythmGameMusic;
        }
    }

    public class RhythmGameMusicSelectMsg : Message
    {
        public int musicIndex;
        public RhythmGameMusicSelectMsg(int musicIndex)
        {
            this.musicIndex = musicIndex;
        }
    }

    public class RhythmGameNoteCreateMsg : Message
    {
        public bool isRigth;
        public int noteCount;
        public RhythmGameNoteCreateMsg(bool isRight, int noteCount)
        {
            this.isRigth = isRight;
            this.noteCount = noteCount;
        }
    }

    public class RhythmGameSetMsg : Message
    {
        public Character mainCharacter;
        public RhythmGameSetMsg(Character character)
        {
            this.mainCharacter = character;
        }
    }

    public class RhythmGameNoteDeleteMsg : Message
    {
        public RhythmNote rhythmNote;
        public int index;
        public GameObject note;
        public bool isRight;
        public bool isNoTouch;
        public RhythmGameNoteDeleteMsg(RhythmNote rhythmNote, int index, GameObject note, bool isRight, bool isNoTouch)
        {
            this.rhythmNote = rhythmNote;
            this.index = index;
            this.note = note;
            this.isRight = isRight;
            this.isNoTouch = isNoTouch;
        }
    }

    public class RhythmGameNoteJudgeMsg : Message
    {
        public RhythmNote rhythmNote;
        public RhythmGameNoteJudgeMsg(RhythmNote rhythmNote)
        {
            this.rhythmNote = rhythmNote;
        }
    }

    public class RhythmGameBtnTouchMsg : Message
    {
        public bool isRight;
        public RhythmGameBtnTouchMsg(bool isRight)
        {
            this.isRight = isRight;
        }
    }

    public class RhythmGameComboMsg : Message
    {
        public int combo;
        public RhythmGameComboMsg(int combo)
        {
            this.combo = combo;
        }
    }

    public class RhythmGameResultMsg : Message
    {
        public int bad;
        public int normal;
        public int good;
        public int perfect;
        public int combo;
        public float score;
        public RhythmGameResultMsg(int bad, int normal, int good, int perfect, int combo, float score)
        {
            this.bad = bad;
            this.normal = normal;
            this.good = good;
            this.perfect = perfect;
            this.combo = combo;
            this.score = score;
        }
    }

    public class RhythmGameResultCloseMsg : Message
    {

    }

    public class RhythmGameMusicSeletCloseMsg : Message
    {

    }

    public class RhytmeGameEndInfoMsg : Message
    {
        public bool isEnd;
        public RhytmeGameEndInfoMsg(bool isEnd)
        {
            this.isEnd = isEnd;
        }

    }

    public class RhythmGameBluetoothSelectMsg : Message
    {
        public bool isBluetoothSelect;
        public RhythmGameBluetoothSelectMsg(bool isBluetoothSelect)
        {
            this.isBluetoothSelect = isBluetoothSelect;
        }
    }
}