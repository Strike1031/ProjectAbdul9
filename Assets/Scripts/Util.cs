using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Util
{
    public static string ParsePath(string path)
    {
        return string.Join("@", path.Split(Path.DirectorySeparatorChar));
    }
}
