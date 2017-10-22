using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Translator : MonoBehaviour
{
	// Словарь переводов
	static Dictionary<string, string> Translate;
	// Текущий язык
	static Languages lang;
	// Запущен ли класс
	public static bool Started;

	// .txt файлы с текстами
	public TextAsset KeysFile;
	public TextAsset[] TranslateFiles;

	// Инициализация для выбранного языка
	public void Init (Languages lng)
	{
		// В случае мультиязычности ищем язык перевода соответствующий языку на устройстве иначе задаем выбранный разработчиком
		if (lng == Languages.MULTI) 
			lang = unityLang2myLang (Application.systemLanguage);
		else
			lang = lng;

		// Считаем сюда ключи и преводы
		string[] keys;
		string[] values;

		// В файлах переводов слова разбиты переносом строки
		keys = KeysFile.text.Split ('\n');
		values = TranslateFiles [(int)lang].text.Split ('\n');

		// Пытаемся отследить некорректное заполнение переводов
		if (keys.Length != values.Length) 
			Debug.LogException (new System.Exception ("Размерности словарей не совпадают"));

		// Забиваем статический словарь полученными строками
		Translate = new Dictionary<string, string> ();
		for (int i = 0; i < keys.Length; i++) 
			Translate.Add (keys [i], values [i]);

		// Класс готов к использованию
		Started = true;
		
	}

	// Запрос перевода
	public static string Trans (string key)
	{
		// Ищем перевод в словаре
		if (Translate.ContainsKey (key)) 
			return Translate [key];
		// Возвращает входную строку, если перевод для нее отсутствует
		return key;
	}

	// Соответствие языков платформ с нашими языками
	Languages unityLang2myLang (SystemLanguage sl)
	{
		switch (sl) {
		case SystemLanguage.Chinese:
			return Languages.Cn;
		case SystemLanguage.German:
			return Languages.Ge;
		case SystemLanguage.English:
			return Languages.En;
		case SystemLanguage.French:
			return Languages.Fr;
		case SystemLanguage.Italian:
			return Languages.It;
		case SystemLanguage.Japanese:
			return Languages.Jp;
		case SystemLanguage.Korean:
			return Languages.Ko;
		case SystemLanguage.Russian:
			return Languages.Ru;
		case SystemLanguage.Spanish:
			return Languages.Sp;
		default:
			return Languages.En;
		}
	}


}

// Языки расставлены в алфавитном порядке
public enum Languages
{
	Cn=0,
	En=1,
	Fr=2,
	Ge=3,
	It=4,
	Jp=5,
	Ko=6,
	Ru=7,
	Sp=8,
	MULTI
}