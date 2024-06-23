# Taskmanager

## Opis

Taskmanager to zaawansowany system do zarządzania zadaniami, który umożliwia użytkownikom efektywną organizację pracy i śledzenie postępów. Dzięki bezpiecznemu dostępowi poprzez autoryzację loginem i hasłem, zapewnia ochronę wszystkich informacji i zadań dodanych do systemu.

## Funkcje

### Bezpieczny dostęp

- **Autoryzacja**: Taskmanager zapewnia bezpieczny dostęp poprzez mechanizm logowania, co gwarantuje ochronę prywatności i bezpieczeństwo danych.

![login](readme/images/login.png)

### Zadania i zlecenia

- **Tworzenie zadań**: Użytkownicy mogą łatwo tworzyć nowe zadania i zlecenia, co umożliwia efektywne zarządzanie workflow i monitorowanie postępów prac.

![add-task](readme/images/add-task.png)

- **Przypisywanie zadań**: Zadania mogą być przypisane do konkretnych użytkowników, co pozwala na klarowne określenie odpowiedzialności.
- **Dodawanie pasków postępu**: Użytkownicy mogą dodawać paski postępu do zadań, co umożliwia wizualne śledzenie stopnia realizacji zadań.
- **Edytowanie zadań**: Zadania mogą być edytowane w celu aktualizacji szczegółów takich jak opis, termin, priorytet i przypisani użytkownicy.

![edit-task-user](readme/images/edit-task-user.png)

- **Usuwanie zadań**: Użytkownicy mogą usuwać zadania, które są już nieaktualne lub zakończone, co pomaga w utrzymaniu porządku w projektach.

### Planowanie pracy

- **Współpraca**: System umożliwia dodawanie nowych pomysłów i dzielenie się nimi z innymi użytkownikami, co wspiera wspólną organizację i planowanie projektów.
- **Harmonogramy i przypomnienia**: Użytkownicy mogą tworzyć harmonogramy zadań oraz ustawiać przypomnienia, co pomaga w terminowym wykonywaniu obowiązków.

### Zarządzanie projektami

- **Tworzenie projektów**: Użytkownicy mogą tworzyć nowe projekty i przypisywać do nich zadania.

![add-project](readme/images/add-project.png)

- **Aktualizacja i usuwanie projektów**: Projekty mogą być aktualizowane i usuwane przez uprawnionych użytkowników.
- **Śledzenie postępów projektów**: Umożliwia monitorowanie postępów prac w projektach i analizowanie efektywności zespołów.

![projects](readme/images/projects.png)

### Zarządzanie użytkownikami

- **Rejestracja i logowanie**: Nowi użytkownicy mogą się rejestrować, a istniejący logować do systemu.
- **Profil użytkownika**: Użytkownicy mogą przeglądać i aktualizować swoje profile oraz zmieniać hasła.

![profile](readme/images/profile.png)

- **Role i uprawnienia**: System zarządza rolami użytkowników (admin, user) i przydziela odpowiednie uprawnienia, co zapewnia kontrolę dostępu do zasobów.

## Instalacja

```bash
# Sklonuj repozytorium
git clone https://github.com/Lefjuu/studia-management

# Przejdź do katalogu projektu
cd taskmanager

# Uruchom aplikację
dotnet build


## Instalacja

```bash
# Sklonuj repozytorium
git clone https://github.com/Lefjuu/studia-management

# Przejdź do katalogu projektu
cd taskmanager

# Uruchom aplikację
dotnet build
```

## Technologie

Projekt Taskmanager wykorzystuje nowoczesne technologie zarówno w części serwerowej, jak i klienckiej, co zapewnia wysoką wydajność i skalowalność systemu.

### Backend

- **ASP.NET Core**: Serwerowa część aplikacji została napisana w ASP.NET Core, co gwarantuje stabilność, bezpieczeństwo oraz efektywne zarządzanie zasobami.
- **JWT**: Używamy JSON Web Tokens do autoryzacji i zabezpieczenia API.
- **MongoDB**: Nierelacyjna baza danych, która oferuje wysoką wydajność, automatyczną skalowalność i łatwość w zarządzaniu dużymi ilościami danych.

### Frontend

- **React**: Interfejs użytkownika został zbudowany przy użyciu biblioteki React, która umożliwia tworzenie dynamicznych i responsywnych aplikacji webowych.

## Użytkowanie

### Logowanie

- Zaloguj się, używając swojego loginu i hasła.

### Zarządzanie zadaniami

- Dodawaj nowe zadania i zlecenia, przypisując im odpowiednie terminy i priorytety.
- Przypisuj zadania konkretnym użytkownikom, aby określić odpowiedzialność za ich realizację.
- Dodawaj paski postępu do zadań, aby wizualnie śledzić postępy.
- Edytuj zadania, aby aktualizować szczegóły takie jak opis, termin, priorytet i przypisani użytkownicy.
- Usuwaj zadania, które są już nieaktualne lub zakończone, aby utrzymać porządek w projektach.

### Śledzenie postępów

- Monitoruj postępy w realizacji zadań i dziel się pomysłami oraz postępami z innymi użytkownikami.

## Backend - Szczegóły implementacji

### Usługi (Services)

#### AuthenticationService

Odpowiada za logikę biznesową związaną z uwierzytelnianiem użytkowników i zarządzaniem ich profilami. Obsługuje rejestrację, logowanie, aktualizację profili oraz zmianę haseł.

#### ProjectService

Odpowiada za zarządzanie projektami. Umożliwia tworzenie, aktualizowanie, usuwanie projektów oraz przypisywanie zadań do projektów. Dzięki tej usłudze możliwe jest także śledzenie postępów w realizacji projektów.

#### TaskService

Odpowiada za zarządzanie zadaniami w projektach. Umożliwia tworzenie, aktualizowanie, usuwanie zadań oraz śledzenie ich postępów. Obsługuje także przypisywanie zadań do konkretnych użytkowników.

## Contributing

Zapraszamy do współpracy! Jeśli masz pomysły, uwagi lub chciałbyś dodać nowe funkcje, otwórz zgłoszenie lub wyślij pull request.Q