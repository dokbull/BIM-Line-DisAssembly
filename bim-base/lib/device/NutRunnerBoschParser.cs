// #define USE_JSON // Newtonsoft.Json 설치 필요

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

#if USE_JSON
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class BOSCH_SCREW_DATA
{
    public class BOSCH_DATA
    {
        public int prorgam_no = 0;
        public string prorgam_name = "";

        public int stepCount = 0;
        public TIGHTENING_STEP[] step = null;
    }

    public class TIGHTENING_STEP
    {
        public int row = 0;
        public string stepName = "";
        public string result = "";
        public int speed = 0;

        public double torqueMinNom = 0.0d; // 최소 설정치
        public double torqueMinAct = 0.0d; // 결과
        public double torqueMaxNom = 0.0d; // 최대 설정치
        public double torqueMaxAct = 0.0d; // 결과

        public double angleMinNom = 0.0d; // 최소 설정치
        public double angleMinAct = 0.0d; // 결과
        public double angleMaxNom = 0.0d; // 최대 설정치
        public double angleMaxAct = 0.0d; // 결과
    }

    public DateTime m_makeTime = DateTime.Now; // 여러개의 데이터 비교 정렬시 사용

    object m_lockObject = new object();
    JObject m_json = new JObject();
    BOSCH_DATA m_data = new BOSCH_DATA();

    public BOSCH_SCREW_DATA()
    {
    }
    
    public void parse(string path)
    {
        lock (m_lockObject)
        {
            string jsonText = File.ReadAllText(path); // 텍스트 파일 읽기

            // JSON 문자열을 JObject로 파싱
            m_json = JObject.Parse(jsonText);
        }

        m_data.prorgam_no = (int)m_json["prg nr"];
        m_data.prorgam_name = m_json["prg name"].ToString();

        JArray tighteningSteps = (JArray)m_json["tightening steps"];

        int count = tighteningSteps.Count;
        m_data.stepCount = count;
        m_data.step = new TIGHTENING_STEP[count];

        for (int i = 0; i < count; i++)
        {
            m_data.step[i] = new TIGHTENING_STEP();
        }

        int cnt = 0;

        foreach (JObject step in tighteningSteps)
        {
            TIGHTENING_STEP tightStep = m_data.step[cnt];

            tightStep.result = (string)step["result"];
            tightStep.speed = (int)step["speed"];
            tightStep.row = (int)step["row"];
            tightStep.stepName = (string)step["name"];

            JArray tighteningFunctions = (JArray)step["tightening functions"]; // 체결 데이터 수집
            foreach (JObject function in tighteningFunctions)
            {
                string functionName = function["name"].ToString();

                if (functionName == "MF TorqueMin" || functionName == "MFs TorqueMin") // TORQUE MIN
                {
                    m_data.step[cnt].torqueMinNom = (double)function["nom"];
                    m_data.step[cnt].torqueMinAct = (double)function["act"];
                }

                if (functionName == "MF TorqueMax" || functionName == "MFs TorqueMax") // TORQUE MAX
                {
                    m_data.step[cnt].torqueMaxNom = (double)function["nom"];
                    m_data.step[cnt].torqueMaxAct = (double)function["act"];
                }

                if (functionName == "MF AngleMin" || functionName == "MFs AngleMin") // ANGLE MIN
                {
                    m_data.step[cnt].angleMinNom = (double)function["nom"];
                    m_data.step[cnt].angleMinAct = (double)function["act"];
                }

                if (functionName == "MF AngleMax" || functionName == "MFs AngleMax") // ANGLE MAX
                {
                    m_data.step[cnt].angleMaxNom = (double)function["nom"];
                    m_data.step[cnt].angleMaxAct = (double)function["act"];
                }
            }

            cnt++;
        }
    }

    public void setMakeTime(DateTime time) // 여러개의 데이터 비교 정렬시 사용
    {
        m_makeTime = time;
    }

    public BOSCH_DATA data()
    {
        return m_data;
    }

} // class

#endif
