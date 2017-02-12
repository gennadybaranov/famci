// MathClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <stdlib.h>
#include "..\MathLibrary\MathLibrary.h"

using namespace std;

class Fraction
{
private:
	int enumerator;
	int denumerator;

public:
	Fraction(int enumerator, int denumenator)
	{
		this->enumerator = enumerator;
		this->denumerator = denumenator;
	}

	Fraction operator-() const {
		Fraction oppositeFraction(-enumerator, denumerator);

		return oppositeFraction;
	}
};



int main()
{
	Fraction f(1, 4);
	Fraction oppositeFraction = -f;
	//srand(3);
	//int i;
	//for (i = 1; i <= 20; i++) {
	//	printf("%d ", rand() % 20);
	//} /* end for */


	//double a = 7.4;
	//int b = 99;

	//MathLibrary::Functions f;

	//cout << "a + b = " << f.Add(a, b) << endl;
	//cout << "a * b = " <<
	//	MathLibrary::Functions::Multiply(a, b) << endl;
	//cout << "a + (a * b) = " <<
	//	MathLibrary::Functions::AddMultiply(a, b) << endl;

    return 0;
}


