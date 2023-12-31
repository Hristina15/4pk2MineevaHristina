﻿// Приведенный ниже блок ifdef — это стандартный метод создания макросов, упрощающий процедуру
// экспорта из библиотек DLL. Все файлы данной DLL скомпилированы с использованием символа MY13_EXPORTS
// Символ, определенный в командной строке. Этот символ не должен быть определен в каком-либо проекте,
// использующем данную DLL. Благодаря этому любой другой проект, исходные файлы которого включают данный файл, видит
// функции MY13_API как импортированные из DLL, тогда как данная DLL видит символы,
// определяемые данным макросом, как экспортированные.
#ifdef MY13_EXPORTS
#define MY13_API __declspec(dllexport)
#else
#define MY13_API __declspec(dllimport)
#endif

// Этот класс экспортирован из библиотеки DLL
class MY13_API CПЗ13 {
public:
	CПЗ13(void);
	// TODO: добавьте сюда свои методы.
};

extern MY13_API int nПЗ13;

MY13_API int fnПЗ13(void);
