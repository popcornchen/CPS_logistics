/*
* MATLAB Compiler: 6.2 (R2016a)
* Date: Wed May 24 14:49:14 2017
* Arguments: "-B" "macro_default" "-W" "dotnet:PSOUpdate,ALGO,0.0,private" "-T"
* "link:lib" "-d" "D:\PROGRAM\CPS_Logistics\CPS_Logistics\MAt\PSOUpdate\for_testing" "-v"
* "class{ALGO:C:\Users\Robin\Desktop\��ҵ��ơ�����CPS�ĳ�������ϵͳ���\PSOUpdate.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace PSOUpdateNative
{

  /// <summary>
  /// The ALGO class provides a CLS compliant, Object (native) interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// C:\Users\Robin\Desktop\��ҵ��ơ�����CPS�ĳ�������ϵͳ���\PSOUpdate.m
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
    /// Provides a single output, 0-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate()
    {
      return mcr.EvaluateFunction("PSOUpdate", new Object[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize);
    }


    /// <summary>
    /// Provides a single output, 2-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence);
    }


    /// <summary>
    /// Provides a single output, 3-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence, Object emergency)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency);
    }


    /// <summary>
    /// Provides a single output, 4-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence, Object emergency, 
                      Object agv1po)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po);
    }


    /// <summary>
    /// Provides a single output, 5-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence, Object emergency, 
                      Object agv1po, Object agv2po)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po);
    }


    /// <summary>
    /// Provides a single output, 6-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence, Object emergency, 
                      Object agv1po, Object agv2po, Object agv1end)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end);
    }


    /// <summary>
    /// Provides a single output, 7-input Objectinterface to the PSOUpdate MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <param name="agv2po">Input argument #5</param>
    /// <param name="agv1end">Input argument #6</param>
    /// <param name="agv2end">Input argument #7</param>
    /// <returns>An Object containing the first output argument.</returns>
    ///
    public Object PSOUpdate(Object swarmsize, Object jobsequence, Object emergency, 
                      Object agv1po, Object agv2po, Object agv1end, Object agv2end)
    {
      return mcr.EvaluateFunction("PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end, agv2end);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] PSOUpdate(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence, 
                        Object emergency)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="swarmsize">Input argument #1</param>
    /// <param name="jobsequence">Input argument #2</param>
    /// <param name="emergency">Input argument #3</param>
    /// <param name="agv1po">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence, 
                        Object emergency, Object agv1po)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po);
    }


    /// <summary>
    /// Provides the standard 5-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
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
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence, 
                        Object emergency, Object agv1po, Object agv2po)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po);
    }


    /// <summary>
    /// Provides the standard 6-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
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
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence, 
                        Object emergency, Object agv1po, Object agv2po, Object agv1end)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end);
    }


    /// <summary>
    /// Provides the standard 7-input Object interface to the PSOUpdate MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
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
    public Object[] PSOUpdate(int numArgsOut, Object swarmsize, Object jobsequence, 
                        Object emergency, Object agv1po, Object agv2po, Object agv1end, 
                        Object agv2end)
    {
      return mcr.EvaluateFunction(numArgsOut, "PSOUpdate", swarmsize, jobsequence, emergency, agv1po, agv2po, agv1end, agv2end);
    }


    /// <summary>
    /// Provides an interface for the PSOUpdate function in which the input and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// ���δ���û�п��ǵ�������Ŷ�С�����ȵ�Ӱ�죬����ά��Ϊ2ά����һά������������AGV̨
    /// ��
    /// �ڶ�ά��AGVִ�������˳�򣬰����������У��������Ϊ�������к�����Ⱥ��ģ����������
    /// ����Ⱥ�Ľ��
    /// ������������Ҳ����˶�̬w��c1��c2�����ĸ����Լ�����Ⱥ�㷨��GA���ϵ��Ż��㷨�����
    /// ﲻ��ѡ��
    /// cycletime ��ѭ������
    /// w��c1��c2 Ϊ����Ⱥ�㷨����
    /// minpath  ����ȫ������·������
    /// legthen Ϊ�������������ʺ���ƴ����
    /// swarmsize Ϊ����Ⱥ������50�Ϳ�����
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("PSOUpdate", 7, 3, 0)]
    protected void PSOUpdate(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("PSOUpdate", numArgsOut, ref argsOut, argsIn, varArgsIn);
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
