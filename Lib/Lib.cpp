#include <iostream>
#include "../ПЗ.12/MathLib.h"



int main()
{
    double a, b;
    std::cin >> a >> b;
    std::cout << "a + b = " << MathLibrary::Arithmetic::Add(a, b) << std::endl;
    std::cin >> a >> b;
    std::cout << "a - b = " << MathLibrary::Arithmetic::Subtract(a, b) << std::endl;
    std::cin >> a >> b;
    std::cout << "a * b = " << MathLibrary::Arithmetic::Multiply(a, b) << std::endl;
    std::cin >> a >> b;
    std::cout << "a / b = " << MathLibrary::Arithmetic::Divide(a, b) << std::endl;
    std::cin >> a >> b;
    std::cout << "a^b = " << MathLibrary::Arithmetic::Exponentiation(a, b) << std::endl;
    std::cin >> a >> b;
    std::cout << "a 1.0/b = " << MathLibrary::Arithmetic::Root(a, b) << std::endl;
}
