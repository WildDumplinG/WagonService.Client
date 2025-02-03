# WagonService.Client

## Описание

## Требования
.NET Core 9.0

## Установка
Для установки проекта выполните следующие шаги:
1. Клонируйте репозиторий:
```
   git clone [https://github.com/WildDumplinG/WagonService.Client.git](https://github.com/WildDumplinG/WagonService.Server)
```
2. Перейдите в каталог скачанного проекта
```bash
  cd WagonService.Client
```
3. Установите необходимые зависимости:
```bash
   dotnet restore
```
# Запуск клиент приложения
```bash
dotnet run --project Client
```
Вводим адрес подключения
Вызываем команду получения вагонов 
```bash
WagonService GetWagons
```
Дату начала
```bash
2024-01-01 00:00:00.00+03
```
Дату конца
```bash
2024-04-01 00:00:00.00+03
```
Получаем данные
```bash
Инвентарный номер: 51371797, Время прибытия: 0001-01-01T00:00:00.0000000, Время отправления: 2024-03-01T17:43:22.6570000Z
```
Релизы находятся по пути [https://github.com/WildDumplinG/WagonService.Client/releases]
