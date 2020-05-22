using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Constants
{
    public enum LocalizingType
    {
        KR,
        JP,
        EN,
        CH,
    }

    public enum Menu
    {
        None,
        Main,
        Watch,
        Music,
        Game,
        HoloStar,
        Store,
        Option,
    }

    public enum GameType
    {
        AlongGame,
        RhythmGame,
    }

    public enum AloneGameMenu
    {
        MainGame,
        Avatar,
        Upgrade,
        Option
    }

    public enum Upgrade
    {
        Skill,
        Buff,
    }

    public enum StoreMenu
    {
        IdolShop,
        SkinShop,
        CashShop,
    }

    public enum Character
    {
        Girl,
        Boy,
    }

    public enum Schedule
    {
        Vocal,
        Dance,
        Entertainment,
        Intelligence,
        Meal,
        Rest
    }

    public enum SetAnimator
    {
        IDLE,
        FACE,
        MOTION,
        ALONGAME
    }

    public enum Weathers
    {
        Clean,
        Snow,
        Rain,
        Cloud,
    }

    //폰이 수신 해야할 데이터와 테블릿 수신 데이터 정리
    public enum SENDMSGTYPE
    {
        //테블릿 수신 정보
        MENU,           //메뉴 정보 메뉴 몇으로 이동
        GAME,           //게임 셀렉트
        MSG,            //기본 메세지 문자 알림
        MUSIC,          //음악 음악정보 추가
        //MUSICLISTINFO,
        CHARINFO,       //케릭터 정보 현재 아바타 복장 등
        LOCATION,       //위치 정보 위치값 
        CALENDAR,       //일정 ??
        SETTING,        //설정 변경 설정 정보
        ANIMATION,      //방치형 게임정보이고 현재 스케줄
        CONNECTION,
        RECEIVE,
        INTENT,
        //모바일 수신 정보
    }

    public enum MUSICINFO
    {
        None,
        MUSIC_LIST,
        MUSIC_PLAY,
        MUSIC_SELECT,
        MUSIC_PAUSE,
        MUSIC_NEXT,
        MUSIC_PREV,
        MUSIC_VOLUME,
        MUSIC_MODE_REPEIT,
        MUSIC_MODE_SHUFFLE,
        MUSIC_TIME,
        //MUSIC_PITCH_UP,
        //MUSIC_PITCH_DOWN,
        //MUSIC_PITCH_STOP,
        //MUSIC_DANCE_NUM
    }

    public enum AnimationType
    {
        //대기
        Idel1,
        Idel2,
        Idel3,
        //스케줄
        Vocal,
        Dance,
        Entertainment,
        Intelligence,
        Meal,
        Rest,
        //face
        Angry,
        Close,
        Sad,
        Smile,
        Surprise,
        Talk,
        //Motioin
        Motion1,
        Motion2,
        Motion3,
        Motion4,
        Motion5,
        Motion6,
        Motion7,
        //Motion8,
        //Motion9,
        //Motion10
    }

    public enum OptionSet
    {
        Bluetooth,
        Schedule,
        TTS,
        SMS
    }

    public enum RhythmNote
    {
        None,
        Bad,
        Normal,
        Good,
        Perfect,
        Miss
    }
}
