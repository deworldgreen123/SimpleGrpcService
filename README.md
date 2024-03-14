# homework-2

# homework-2

Необходимо разработать сервис для хранения информации о товарах.
(Сервис должен хранить данные о товарах в памяти)

Сущности:

Товар
{
    Номер товара;
    Наименование товара;
    Цена товара;
    Вес товара;
    Вид товара{Общий, Бытовая химия, Техника, Продукты};
    Дата создания;
    Номер Склада;
}


Нужно реализовывать Grpc-сервис со следующими методами:

* Создать товар;
* Получить список товаров с фильтрами (Дата создания, Вид товара, Склад) и Пагинацией ();
* Получить товар по Id;
* Обновить цену товара;

Дополнительно (за все пункт один 💎):
* Реализовать Interceptor для логирования Request и Response;
* Реализовать Interceptor для обработки ошибок;
* Реализовать валидацию товаров через FluentValudation
* Реализовать мапнг Grpc моделей во внутренние через Automapper


Задание со звездочкой 💎: Реализовать одновременно и asp.net и grpc-сервис

При разработке не использовать никаких внешних источников данных (БД, редис и т.п.). 
Все данные хранить в оперативной памяти. Данные должны быть доступны только на время работы сервиса. Допускается расширить функциональность сервисов по своему усмотрению, но минимальный набор требований должен быть выполнен обязательно.

Критерии корректно выполнения задания: 
1. Все ручки реализованы 
2. Код должен быть написан чисто, разделен на логические блоки. Желательно использовать стандартные настройки code style в райдере.


