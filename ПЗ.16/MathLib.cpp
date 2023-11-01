#include "framework.h"
#include "MathLib.h"
#include <math.h>
#include "targetver.h"


namespace MathLibrary
{
	double Arithmetic::Add(double a, double b)
	{
		return a + b;
	}
	double Arithmetic::Subtract(double a, double b)
	{
		return a - b;
	}
	double Arithmetic::Multiply(double a, double b)
	{
		return a * b;
	}
	double Arithmetic::Divide(double a, double b)
	{
		return a / b;
	}
	double Arithmetic::Exponentiation(double a, double b)
	{
		return pow(a, b);
	}
	double Arithmetic::Root(double a, double b)
	{
		return pow(a, 1.0 / b);
	}
}