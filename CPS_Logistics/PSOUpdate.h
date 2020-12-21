/*
 * MATLAB Compiler: 6.2 (R2016a)
 * Date: Fri Apr 14 13:27:21 2017
 * Arguments: "-B" "macro_default" "-W" "lib:PSOUpdate" "-T" "link:lib" "-d"
 * "C:\Users\Robin\Desktop" "-v" "C:\Users\Robin\Desktop\PSOUpdate.m" 
 */

#ifndef __PSOUpdate_h
#define __PSOUpdate_h 1

#if defined(__cplusplus) && !defined(mclmcrrt_h) && defined(__linux__)
#  pragma implementation "mclmcrrt.h"
#endif
#include "mclmcrrt.h"
#ifdef __cplusplus
extern "C" {
#endif

#if defined(__SUNPRO_CC)
/* Solaris shared libraries use __global, rather than mapfiles
 * to define the API exported from a shared library. __global is
 * only necessary when building the library -- files including
 * this header file to use the library do not need the __global
 * declaration; hence the EXPORTING_<library> logic.
 */

#ifdef EXPORTING_PSOUpdate
#define PUBLIC_PSOUpdate_C_API __global
#else
#define PUBLIC_PSOUpdate_C_API /* No import statement needed. */
#endif

#define LIB_PSOUpdate_C_API PUBLIC_PSOUpdate_C_API

#elif defined(_HPUX_SOURCE)

#ifdef EXPORTING_PSOUpdate
#define PUBLIC_PSOUpdate_C_API __declspec(dllexport)
#else
#define PUBLIC_PSOUpdate_C_API __declspec(dllimport)
#endif

#define LIB_PSOUpdate_C_API PUBLIC_PSOUpdate_C_API


#else

#define LIB_PSOUpdate_C_API

#endif

/* This symbol is defined in shared libraries. Define it here
 * (to nothing) in case this isn't a shared library. 
 */
#ifndef LIB_PSOUpdate_C_API 
#define LIB_PSOUpdate_C_API /* No special import/export declaration */
#endif

extern LIB_PSOUpdate_C_API 
bool MW_CALL_CONV PSOUpdateInitializeWithHandlers(
       mclOutputHandlerFcn error_handler, 
       mclOutputHandlerFcn print_handler);

extern LIB_PSOUpdate_C_API 
bool MW_CALL_CONV PSOUpdateInitialize(void);

extern LIB_PSOUpdate_C_API 
void MW_CALL_CONV PSOUpdateTerminate(void);



extern LIB_PSOUpdate_C_API 
void MW_CALL_CONV PSOUpdatePrintStackTrace(void);

extern LIB_PSOUpdate_C_API 
bool MW_CALL_CONV mlxPSOUpdate(int nlhs, mxArray *plhs[], int nrhs, mxArray *prhs[]);



extern LIB_PSOUpdate_C_API bool MW_CALL_CONV mlfPSOUpdate(int nargout, mxArray** optswarm, mxArray** agv1, mxArray** agv2, mxArray* swarmsize, mxArray* jobsequence, mxArray* emergency);

#ifdef __cplusplus
}
#endif
#endif
