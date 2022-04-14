#include "pch.h"
#include <stdint.h>

extern "C" {

	double __declspec(dllexport) Sum(double left, double right)
	{
		return left + right;
	}

	int64_t __declspec(dllexport) SumInt(int64_t left, int64_t right)
	{
		return left + right;
	}

	double __declspec(dllexport) Subtract(double left, double right) 
	{
		return left - right;
	}

	int64_t __declspec(dllexport) SubtractInt(int64_t left, int64_t right)
	{
		return left - right;
	}

	int64_t __declspec(dllexport) MultiplyInt(int64_t left, int64_t right)
	{
		return left * right;
	}

	double __declspec(dllexport) Multiply(double left, double right)
	{
		return left * right;
	}

	double __declspec(dllexport) Divide(double left, double right)
	{
		return left / right;
	}
	
	int64_t __declspec(dllexport) Mod(int64_t left, int64_t right)
	{
		return left % right;
	}

	double __declspec(dllexport) Factorial(int64_t left)
	{
		double res = 1;
		for (int64_t i = left; i > 0; i--) {
			res *= left;
		}
		return res;
	}

}






