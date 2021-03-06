/*
* MATLAB Compiler: 6.2 (R2016a)
* Date: Wed May 24 14:49:14 2017
* Arguments: "-B" "macro_default" "-W" "dotnet:PSOUpdate,ALGO,0.0,private" "-T"
* "link:lib" "-d" "D:\PROGRAM\CPS_Logistics\CPS_Logistics\MAt\PSOUpdate\for_testing" "-v"
* "class{ALGO:C:\Users\Robin\Desktop\毕业设计—基于CPS的车间物流系统设计\PSOUpdate.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace PSOUpdate
{

  /// <summary>
  /// The ALGO class provides a CLS compliant, MWArray interface to the MATLAB functions
  /// contained in the files:
  /// <newpara></newpara>
  /// C:\Users\Robin\Desktop\毕业设计—基于CPS的车间物流系统设计\PSOUpdate.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class ALGO : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Runtime instance.
    /// </summary>
    static ALGO()
    {
      if (MWMCR.MCRAppInitialized)
      {
        try
        {
          Assembly assembly= Assembly.GetExecutingAssembly();

          string ctfFilePath= assembly.Location;

          int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

          ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

          string ctfFileName = "PSOUpdate.ctf";

          Stream embeddedCtfStream = null;

          String[] resourceStrings = assembly.GetManifestResourceNames();

          foreach (String name in resourceStrings)
          {
            if (name.Contains(ctfFileName))
            {
              embeddedCtfStream = assembly.GetManifestResourceStream(name);
              break;
            }
          }
          mcr= new MWMCR("",
                         ctfFilePath, embeddedCtfStream, true);
        }
        catch(Exception ex)
        {
          ex_ = new Exception("MWArray assembly failed to be initialized", ex);
        }
      }
      else
      {
        ex_ = new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the ALGO class.
    /// </summary>
    public ALGO()
    {
      if(ex_ != null)
      {
        throw ex_;
      }
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~ALGO()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate()
    {
      return mcr.EvaluateFunction("PSOUpdate", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence, MWArray emergency)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence, MWArray emergency, 
                       MWArray agv1po)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence, MWArray emergency, 
                       MWArray agv1po, MWArray agv2po)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence, MWArray emergency, 
                       MWArray agv1po, MWArray agv2po, MWArray agv1end)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <param name="agv2end">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PSOUpdate(MWArray swarmsize, MWArray jobsequence, MWArray emergency, 
                       MWArray agv1po, MWArray agv2po, MWArray agv1end, MWArray agv2end)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end, agv2end);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence, 
                         MWArray emergency)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence, 
                         MWArray emergency, MWArray agv1po)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence, 
                         MWArray emergency, MWArray agv1po, MWArray agv2po)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence, 
                         MWArray emergency, MWArray agv1po, MWArray agv2po, MWArray 
                         agv1end)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <param name="agv2end">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PSOUpdate(int numArgsOut, MWArray swarmsize, MWArray jobsequence, 
                         MWArray emergency, MWArray agv1po, MWArray agv2po, MWArray 
                         agv1end, MWArray agv2end)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end, agv2end);
    }


    /// <summary>
    /// Provides an interface for the PSOUpdate function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// 本段代码没有考虑到车间干扰对小车调度的影响，粒子维数为2维，第一维是整数，代表AGV台
    /// 号
    /// 第二维是AGV执行任务的顺序，按照升序排列，输入参数为工作序列和粒子群规模，输出参数�
    /// 鞫群蟮慕峁�
    /// 在其他文献中也提出了动态w，c1，c2参数的概念以及粒子群算法与GA相结合的优化算法，这�
    /// 锊挥柩≡瘛�
    /// cycletime 是循环次数
    /// w，c1，c2 为粒子群算法参数
    /// minpath  储存全局最优路径长度
    /// legthen 为任务数量，单词好像拼错了
    /// swarmsize 为粒子群数量，50就可以了
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void PSOUpdate(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("PSOUpdate", numArgsOut, ref argsOut, argsIn);
    }



    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private static Exception ex_= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
