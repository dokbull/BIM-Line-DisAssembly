using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

/// <summary>
/// 지원 XML SyntaxVersion 은 현재 5.0 임 (LMS V5)
/// </summary>
public class CLMSXml
{
    XmlDocument m_doc = null;

    public int trackCount = 0;
    public string syntexVersion = "";
    public string machineName = "";

    public struct TrippleFuncStruct
    {
        public int analogSlotId;
        public int analogInputNr;
        public int muxId;
    }

    public class SensorData
    {
        public double sensorPos;
        public double cmCommutationAngle;

        public double interpSinOffset;
        public double interpCosOffset;
        public double interpSinAmplitude;
        public double interpCosAmplitude;
        public double interpSinCosPhaseCorrection;

        public double sensorDetectCrit;
        public double sensorDetectHyst;
        public double sensorOrientation;

        public double filterTimeConst;
        public double filterCriterion;

        public double maxSpeedDuration;
        public double maxSpeedCriterion;

        public TrippleFuncStruct sine;
        public TrippleFuncStruct cosine;
    }

    public class CoilData
    {
        public string name;
        public string coilParFile;
        public int defaultTrack;
        public int nrOfCoilTriplets;
        public double coilTripletCoverageFraction;
        public double controlHysteresis;
        public double positionOffset;
        public double sensorSwitchMargin;
        public double sensorPosOffset;
        public double sensorTransitionZone;
        public double controllerIntegralGain;
        public double controllerProportionalGain;
        public double controllerProportionalGainInError;
        public double filterFrequency;
        public double filterDamping;
        public double saturationLevel;
        public int nrOfSensors;

        public List<SensorData> sensor = new List<SensorData>();
    }

    public class TrackCoilData
    {
        public string name;
        public string coilCoggingParFile;
        public double coilPosition;
    }

    public class TrackData
    {
        public int id;
        public int nrOfCoils;
        public int nrOfSharedCoilsBegin;
        public int nrOfSharedCoilsEnd;
        public int nrOfBumpers;
        public double interpFactor;
        public int sensorType;

        public double motorMagnetPolePitch;
        public int motorMagnetNrOfPolePairs;
        public int motorMagnetPoleConfiguration;
        public double motorMagnetEccentricity;

        public double measurementScaleSensorPeriod;
        public int measurementScaleNrOfPeriods;
        public double measurementScaleEccentricity;

        public double carrierLength;
        public double coilTripletPitch;
        public double trackType;
        public double trackBeginPos;
        public double trackEndPos;
        
        public int homeMode;
        public double homeVelocity;
        public int homeTimeout;
        public double homeMoveCheckDelay;
        public double maxVelocity;
        public double maxAcceleration;
        public double maxJerk;

        public List<TrackCoilData> coil = new List<TrackCoilData>();
    }

    public List<CoilData> coil = new List<CoilData>();
    public List<TrackData> track = new List<TrackData>();

    public CLMSXml(string path)
    {
        m_doc = new XmlDocument();
        m_doc.Load(path);

        XmlNodeList infoNode = m_doc.GetElementsByTagName("LMS");

        foreach (XmlNode item in infoNode)
        {
            syntexVersion = item["syntaxVersion"].InnerText;
            trackCount = Util.toInt32(item["nrOfTracks"].InnerText);
            machineName = item["machineName"].InnerText;
        }

        XmlNodeList carrierPars = m_doc.SelectNodes("LMS/carrierPars");

        int coilCount = 1;

        while (true) // coilCount 는 따로 지정되어있지 않으므로 전체 검색함
        {
            string nodeText = "LMS/coils/c" + coilCount.ToString("D2");
            XmlNodeList coils = m_doc.SelectNodes(nodeText);

            if (coils.Count == 0)
                break;

            parseCoil(coils, nodeText);
            coilCount++;
        }

        for (int i = 0; i < trackCount; i++)
        {
            string nodeText = "LMS/track" + i.ToString();
            XmlNodeList tracks = m_doc.SelectNodes(nodeText);

            parseTrack(i, tracks, nodeText);
        }
            
    }

    public void parseCoil(XmlNodeList nodeList, string nodeText)
    {
        CoilData d = new CoilData();

        foreach (XmlNode item in nodeList)
        {
            d.name = item.toString("name");
            d.coilParFile = item.toString("coilParFile"); //TODO@tmdwn..COIL PAR FILE 추가 해석 해야 함

            d.defaultTrack = item.toInt("defaultTrack");
            d.nrOfCoilTriplets = item.toInt("nrOfCoilTriplets");
            d.coilTripletCoverageFraction = item.toDouble("coilTripletCoverageFraction");

            d.controlHysteresis = item.toDouble("controlHysteresis");
            d.positionOffset = item.toDouble("positionOffset");
            d.sensorSwitchMargin = item.toDouble("sensorSwitchMargin");
            d.sensorPosOffset = item.toDouble("sensorPosOffset");
            d.sensorTransitionZone = item.toDouble("sensorTransitionZone");
            d.controllerIntegralGain = item.toDouble("controllerIntegralGain");
            d.controllerProportionalGain = item.toDouble("controllerProportionalGain");
            d.controllerProportionalGainInError = item.toDouble("controllerProportionalGainInError");
            d.filterFrequency = item.toDouble("filterFrequency");
            d.filterDamping = item.toDouble("filterDamping");
            d.saturationLevel = item.toDouble("saturationLevel");

            d.nrOfSensors = Util.toInt32(item["nrOfSensors"].InnerText);

            coil.Add(d);
        }

        for (int i = 0; i < d.nrOfSensors; i++)
        {
            string subNodeText = nodeText + "/sensor" + i.ToString();
            XmlNodeList sensors = m_doc.SelectNodes(subNodeText);
            parseCoilSensor(sensors, subNodeText, d);
        }
    }

    public void parseCoilSensor(XmlNodeList nodeList, string nodeText, CoilData coil)
    {
        SensorData d = new SensorData();

        foreach (XmlNode item in nodeList)
        {
            d.sensorPos = item.toDouble("sensorPos");
            d.cmCommutationAngle = item.toDouble("cmCommutationAngle");
            d.interpSinOffset = item.toDouble("interpSinOffset");
            d.interpCosOffset = item.toDouble("interpCosOffset");
            d.interpSinAmplitude = item.toDouble("interpSinAmplitude");
            d.interpCosAmplitude = item.toDouble("interpCosAmplitude");

            d.interpSinCosPhaseCorrection = item.toDouble("interpSinCosPhaseCorrection");
            d.sensorDetectCrit = item.toDouble("sensorDetectCrit");
            d.sensorDetectHyst = item.toDouble("sensorDetectHyst");
            d.sensorOrientation = item.toDouble("sensorOrientation");
            d.filterTimeConst = item.toDouble("filterTimeConst");
            d.filterCriterion = item.toDouble("filterCriterion");
            d.maxSpeedDuration = item.toDouble("maxSpeedDuration");
            d.maxSpeedCriterion = item.toDouble("maxSpeedCriterion");

            d.sine.analogSlotId = item["sine"].toInt("analogSlotId");
            d.sine.analogInputNr = item["sine"].toInt("analogInputNr");
            d.sine.muxId = item["sine"].toInt("muxId");

            d.cosine.analogSlotId = item["cosine"].toInt("analogSlotId");
            d.cosine.analogInputNr = item["cosine"].toInt("analogInputNr");
            d.cosine.muxId = item["cosine"].toInt("muxId");

            coil.sensor.Add(d);
        }
    }

    public void parseTrack(int trackNo, XmlNodeList nodeList, string nodeText)
    {
        TrackData d = new TrackData();
        d.id = trackNo;

        foreach (XmlNode item in nodeList)
        {
            d.nrOfCoils = item.toInt("nrOfCoils");
            d.nrOfSharedCoilsBegin = item.toInt("nrOfSharedCoilsBegin");
            d.nrOfSharedCoilsEnd = item.toInt("nrOfSharedCoilsEnd");
            d.nrOfBumpers = item.toInt("nrOfBumpers");
            d.interpFactor = item.toDouble("interpFactor");
            d.sensorType = item.toInt("sensorType");
            d.motorMagnetPolePitch = item.toDouble("motorMagnetPolePitch");
            d.motorMagnetNrOfPolePairs = item.toInt("motorMagnetNrOfPolePairs");
            d.motorMagnetPoleConfiguration = item.toInt("motorMagnetPoleConfiguration");
            d.motorMagnetEccentricity = item.toDouble("motorMagnetEccentricity");
            d.measurementScaleSensorPeriod = item.toDouble("measurementScaleSensorPeriod");
            d.measurementScaleNrOfPeriods = item.toInt("measurementScaleNrOfPeriods");
            d.measurementScaleEccentricity = item.toDouble("measurementScaleEccentricity");
            d.carrierLength = item.toDouble("carrierLength");
            d.coilTripletPitch = item.toDouble("coilTripletPitch");
            d.trackType = item.toDouble("trackType");
            d.trackBeginPos = item.toDouble("trackBeginPos");
            d.trackEndPos = item.toDouble("trackEndPos");
            d.homeMode = item.toInt("homeMode");
            d.homeVelocity = item.toDouble("homeVelocity");
            d.homeTimeout = item.toInt("homeTimeout");
            d.homeMoveCheckDelay = item.toDouble("homeMoveCheckDelay");
            d.maxVelocity = item.toDouble("maxVelocity");
            d.maxAcceleration = item.toDouble("maxAcceleration");
            d.maxJerk = item.toDouble("maxJerk");
        }

        track.Add(d);

        for (int i = 0; i < d.nrOfCoils; i++)
        {
            string subNodeText = nodeText + "/coil" + i.ToString() + "/alt0";
            XmlNodeList sensors = m_doc.SelectNodes(subNodeText);
            parseTrackCoil(sensors, subNodeText, d);
        }
    }

    public void parseTrackCoil(XmlNodeList nodeList, string nodeText, TrackData track)
    {
        TrackCoilData d = new TrackCoilData();
        foreach (XmlNode item in nodeList)
        {
            d.name = item.toString("name");
            d.coilCoggingParFile = item.toString("coilCoggingParFile");
            d.coilPosition = item.toDouble("coilPosition");
        }

        track.coil.Add(d);
    }

} // class


public static class ExtXmlNode
{
#if false
    public static double toDouble(this XmlElement element)
    {
        return Util.toDouble(element.InnerText);
    }

    public static int toInt(this XmlElement element)
    {
        return Util.toInt32(element.InnerText);
    }
#endif

#if false
    public static int toInt(this XmlNode item)
    {
        return Util.toInt32(item.InnerText);
    }

    public static double toDouble(this XmlNode item)
    {
        return Util.toDouble(item.InnerText);
    }

    public static string toString(this XmlNode item)
    {
        return item.InnerText;
    }
#endif

    public static int toInt(this XmlNode item, string name)
    {
        return Util.toInt32(item[name].InnerText);
    }

    public static double toDouble(this XmlNode item, string name)
    {
        return Util.toDouble(item[name].InnerText);
    }

    public static string toString(this XmlNode item, string name)
    {
        return item[name].InnerText;
    }
}