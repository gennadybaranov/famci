// MathClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "..\MathLibrary\MathLibrary.h"

using namespace std;


int main()
{
	double a = 7.4;
	int b = 99;

	MathLibrary::Functions f;

	cout << "a + b = " << f.Add(a, b) << endl;
	cout << "a * b = " <<
		MathLibrary::Functions::Multiply(a, b) << endl;
	cout << "a + (a * b) = " <<
		MathLibrary::Functions::AddMultiply(a, b) << endl;

    return 0;
}

