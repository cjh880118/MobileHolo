using CellBig.Constants;

namespace CellBig.Models
{
    public class GameModel : Model
    {
        //여기에는 테이블 같은 정적 데이터(모드별 Default 값)
        public ModelRef<SettingModel> setting = new ModelRef<SettingModel>();
        public ModelRef<ScheduleModel> schedule = new ModelRef<ScheduleModel>();
        public ModelRef<PlayerInventoryModel> playerInventoryModel = new ModelRef<PlayerInventoryModel>();
        public ModelRef<PlayerStatusModel> playerStatusModel = new ModelRef<PlayerStatusModel>();
        public ModelRef<HolostarSettingModel> holostartSettingModel = new ModelRef<HolostarSettingModel>();
        public ModelRef<NoteModel> noteModel = new ModelRef<NoteModel>();
        public ModelRef<MobileOptionModel> optionModel = new ModelRef<MobileOptionModel>();
        public ModelRef<CalendarModel> calendarModel = new ModelRef<CalendarModel>();
     
        #region [ GameData ]

        int StartTime = 0;
        public int GetStartTime
        {
            get { return StartTime; }
        }

        #endregion
        public void Setup()
        {
            setting.Model = new SettingModel();
            setting.Model.Setup(this);

            schedule.Model = new ScheduleModel();
            schedule.Model.Setup(this);

            playerInventoryModel.Model = new PlayerInventoryModel();
            playerInventoryModel.Model.Setup(this);

            playerStatusModel.Model = new PlayerStatusModel();
            playerStatusModel.Model.Setup(this);

            holostartSettingModel.Model = new HolostarSettingModel();
            holostartSettingModel.Model.Setup(this);

            noteModel.Model = new NoteModel();
            noteModel.Model.Setup(this);

            optionModel.Model = new MobileOptionModel();
            optionModel.Model.Setup(this);

            calendarModel.Model = new CalendarModel();
            calendarModel.Model.Setup(this);
        }
    }
}

