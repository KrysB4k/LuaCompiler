#include "pch.h"
#include "dllmain.h"
#include "lua/lua.hpp"

extern "C"
{
    void CompileFile(const char* in, const char* out)
    {
        Compile(in, out);
    }

    void ErrorMessage(const char* msg)
    {
        MessageBoxA(NULL, msg, "Error", MB_OK | MB_ICONERROR);
    }
}
