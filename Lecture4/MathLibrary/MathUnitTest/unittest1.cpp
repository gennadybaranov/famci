#include "stdafx.h"
#include "CppUnitTest.h"
#include "..\MathLibrary\MathLibrary.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace MathUnitTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(Add)
		{
			//Arrange
			MathLibrary::Functions f;
			int a = 1;
			int b = 2;
			int expected = 3;

			//Act
			int sum = f.Add(a, b);

			//Assert
			Assert::AreEqual(expected, sum);
		}
	};
}