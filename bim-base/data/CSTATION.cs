namespace bim_base
{
    public class CSTATION
    {
        public enum STATION
        {
            NONE = 0,
            IN_WORK_CV,
            ALIGN_CV,
            IN_PP,
            MOLD_REVERSE,
            SHUTTLE_ST_1,
            SHUTTLE_ST_2,
            SHUTTLE_ST_3,
            MOLD_PP_LEFT,
            MOLD_PP_RIGHT,
            UB_PP,
            UB_REVERSE_FRONT,
            UB_REVERSE_REAR,
            OUT_MOLD_CV,
            OUT_UB_CV_FRONT,
            OUT_UB_CV_REAR, 
            MAX,
        }

        public enum TYPE
        {
            EMPTY,
            PRE_MOLD, // before user work
            MOLD, // after user work
            COVER, // upper cover
            BASE_UB, // under base (with ub)
            BASE, // under base (without ub)
            UB, // display
        }

        STATION m_station = STATION.MAX;
        TYPE m_type = TYPE.EMPTY;
        string m_bcr = "";
        bool m_pass = true;

        public CSTATION(STATION st)
        {
            m_station = st;
        }

        public void setType(TYPE type)
        {
            m_type = type;
            Conf.setLastStationType((int)m_station, type);
        }

        public TYPE type()
        {
            return m_type;
        }

        public void setBcr(string bcr)
        {
            m_bcr = bcr;
            Conf.setLastStationBcr((int)m_station, bcr);
        }

        public string bcr()
        {
            return m_bcr;
        }

        public void setPass(bool value)
        {
            m_pass = value;
            Conf.setLastStationPass((int)m_station, value);
        }

        public bool pass()
        {
            return m_pass;
        }

        public void move(CSTATION target)
        {
            target.copy(this);
            clear();
        }

        public void copy(CSTATION source)
        {
            m_type = source.m_type;
            m_bcr = source.m_bcr;
            m_pass = source.m_pass;

            Conf.setLastStationType((int)m_station, m_type);
            Conf.setLastStationBcr((int)m_station, m_bcr);
            Conf.setLastStationPass((int)m_station, m_pass);
        }

        public void clear()
        {
            m_type = TYPE.EMPTY;
            m_bcr = "";
            m_pass = true;

            Conf.setLastStationType((int)m_station, m_type);
            Conf.setLastStationBcr((int)m_station, m_bcr);
            Conf.setLastStationPass((int)m_station, m_pass);
        }
    }
}
