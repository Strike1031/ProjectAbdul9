// This class is for parameters passed with message.
// Remarks:
//      Parameters only be main value type like int, float, string and bool.
//      The number of any type parameters are not restricted.
// Usage:
//      You first can instantiate new MsgParam object and then add any type of parameters
//      by calling Ints(), Floats(), Strings() and Bools().
//      And in callback method you can retrieve these values with IntVal, FloatVal, StringVal and
//      BoolVal properties.
//      If count of every parameter are optionally changed, you must check its count with IntCount,
//      FloatCount, StringCount and BoolCount properties before access special index of parameter.
// Notice:
//      Calling these methods will clear same type values and add new values listed in method.
//      parameter.
// Sample:
//      Making parameter can be like as following.
//          MsgParam mp = new MsgParam().
//              Ints(0, 1, 100).
//              Floats(0.0f, 2.2f).
//              Strings("sample", "message").
//              Bools(true, false);
//      Then can accessed like this.
//          mp.GetInt[0];
//      or
//          if (mp.FloatCount() > 2)
//              floatVal = mp.FloatVal[2];

public class MsgParam
{
    public static MsgParam Empty = new MsgParam();

    private int[] m_ints = null;
    private float[] m_floats = null;
    private string[] m_strings = null;
    private bool[] m_bools = null;
    private object[] m_objects = null;
    private uint[] m_uints = null;

    public int[] IntVal
    {
        get { return m_ints; }
    }

    public float[] FloatVal
    {
        get { return m_floats; }
    }

    public string[] StringVal
    {
        get { return m_strings; }
    }

    public bool[] BoolVal
    {
        get { return m_bools; }
    }

    public object[] ObjectVal
    {
        get { return m_objects; }
    }

    public uint[] UIntVal
    {
        get { return m_uints; }
    }

    public int IntCount
    {
        get { return m_ints == null ? 0 : m_ints.Length; }
    }

    public int FloatCount
    {
        get { return m_floats == null ? 0 : m_floats.Length; }
    }

    public int StringCount
    {
        get { return m_strings == null ? 0 : m_strings.Length; }
    }

    public int BoolCount
    {
        get { return m_bools == null ? 0 : m_bools.Length; }
    }

    public int ObjCount
    {
        get { return m_objects == null ? 0 : m_objects.Length; }
    }

    public int UIntCount
    {
        get { return m_uints == null ? 0 : m_uints.Length; }
    }

    public MsgParam()
    {

    }

    public MsgParam(MsgParam originObj)
    {
        m_ints = originObj.IntVal;
        m_floats = originObj.FloatVal;
        m_strings = originObj.StringVal;
        m_bools = originObj.BoolVal;
        m_objects = originObj.ObjectVal;
        m_uints = originObj.UIntVal;
    }

    public static MsgParam newFigureParam(
        int _wLowParam,
        int _wHiParam,
        int _lLowParam,
        int _lHiParam,
        string _figureImgPath)
    {
        return new MsgParam().FigureParam(_wLowParam, _wHiParam, _lLowParam, _lHiParam, _figureImgPath);
    }

    public static MsgParam newFigureParam(
        int _wLowParam,
        int _wHiParam,
        int _lLowParam,
        string _figureImgPath)
    {
        return new MsgParam().FigureParam(_wLowParam, _wHiParam, _lLowParam, 0, _figureImgPath);
    }

    public MsgParam FigureParam(
        int _wLowParam,
        int _wHiParam,
        int _lLowParam,
        int _lHiParam,
        string _figureImgPath)
    {
        return this.Ints(_wLowParam, _wHiParam, _lLowParam, _lHiParam).Strings(_figureImgPath);
    }

    public MsgParam Ints(params int[] _ints)
    {
        m_ints = _ints;

        return this;
    }

    public static MsgParam newInts(params int[] _ints)
    {
        return new MsgParam().Ints(_ints);
    }

    public MsgParam Floats(params float[] _floats)
    {
        m_floats = _floats;

        return this;
    }
    public static MsgParam newFloats(params float[] _floats)
    {
        return new MsgParam().Floats(_floats);
    }

    public MsgParam Strings(params string[] _strings)
    {
        m_strings = _strings;

        return this;
    }
    public static MsgParam newStrings(params string[] _strings)
    {
        return new MsgParam().Strings(_strings);
    }

    public MsgParam Bools(params bool[] _bools)
    {
        m_bools = _bools;

        return this;
    }

    public static MsgParam newBools(params bool[] _bools)
    {
        return new MsgParam().Bools(_bools);
    }


    public MsgParam Objs(params object[] _objs)
    {
        m_objects = _objs;

        return this;
    }

    public static MsgParam newObjs(params object[] _objs)
    {
        return new MsgParam().Objs(_objs);
    }

    public MsgParam UInts(params uint[] _uints)
    {
        m_uints = _uints;

        return this;
    }

    public static MsgParam newUInts(params uint[] _uints)
    {
        return new MsgParam().UInts(_uints);
    }
}