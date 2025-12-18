# Análisis del proyecto: NBTMap-Explorer

Fecha: diciembre 16, 2025  
Branch activo: `main` (repositorio en `C:\Users\Jorge\source\repos\NBTMap-Explorer`)

## Archivos revisados
- `README.md`
- `features.md`
- `App.xaml.cs`
- `NBTMap-Explorer/Helpers/Minecraft.cs`
- `NBTMap-Explorer/Views/BaseWindow.xaml.cs`
- `NBTMap-Explorer/Views/SplashScreen.xaml.cs`
- `NBTMap-Explorer/ViewModels/SplashScreenViewModel.cs`
- `NBTMap-Explorer/ViewModels/ViewModelBase.cs`
- `NBTMap-Explorer/ViewModels/Interfaces/RelayCommand.cs`
- `NBTMap-Explorer/Config/PreferencesManager.cs`
- Tests: `Tests/MinecraftJava.cs`, `Tests/ExampleTest.cs`
- `AssemblyInfo.cs`, `MIT-LICENSE.txt`

---

## Resumen ejecutivo
El proyecto sigue el patrón MVVM y tiene una buena base (Serilog, estructura de carpetas, intención de tests). Existen áreas claras de mejora en validación, seguridad de IO, calidad de tests y consistencia de estilo. A continuación se listan hallazgos detallados y acciones recomendadas.

---

## Qué tienes que mejorar (prioritario)
- Validación de rutas y persistencia:
  - `PreferencesManager.CustomMinecraftJavaEditionPath` escribe cualquier cadena sin validar existencia/permisos. Añadir validación y manejo de errores antes de `Save()`.
- Tests dependientes del entorno:
  - `Tests/MinecraftJava.cs::IsInstalled` asume que Minecraft está instalado; fragiliza CI. Sustituir por abstracciones e inyectar fakes/mocks.
  - `ExampleTest` es un placeholder; crear tests significativos o eliminar.
- Manejo de `INotifyPropertyChanged`:
  - `ViewModelBase.OnPropertyChanged(string)` usa literales. Cambiar a `CallerMemberName` para reducir errores en refactors.
- Rutas de logging y permisos:
  - `App.xaml.cs` escribe logs en `logs/log-.txt` (ruta relativa). Si la app se instala en ubicaciones protegidas fallará. Usar `Environment.SpecialFolder.LocalApplicationData`.
- Nombres y ortografía:
  - `isPreseed` parece typo; renombrar a `isPressed` o `isDarkThemeActive`.
  - Corregir errores tipográficos en documentación (`Audicence`, `Sucess`, `Rendeer`, `ony`, `craeting`).
- Código nativo y responsabilidades:
  - P/Invoke (structs y métodos) están mezclados en `BaseWindow`. Extraer a `NativeMethods` o clase dedicada para aislar y facilitar pruebas.
- Nullable y modernización:
  - Habilitar `Nullable` en `.csproj` y resolver avisos para mayor seguridad de tipos.

---

## Problemas de seguridad (potenciales)
- Información sensible en logs:
  - Evitar loggear rutas completas de usuario o contenido de archivos en niveles `Information` o `Debug`.
- Escritura en directorios inseguros:
  - Evitar usar rutas relativas que puedan requerir elevación; preferir `LocalApplicationData`.
- Validación insuficiente:
  - Validar entradas de usuario (rutas personalizadas). Evitar caminos con traversal o accesos no deseados.
- Tests que manipulan el FS real:
  - Usar directorios temporales controlados y limpiar siempre; preferir mocks para evitar efectos colaterales.

---

## Mal código / code smells detectados
- Literales para `OnPropertyChanged` (propenso a roturas en renombrado).
- Booleans con nombres confusos/typos (`isPreseed`).
- P/Invoke y structs definidos dentro de una clase de UI en lugar de módulo `NativeMethods`.
- Utilidades con responsabilidades mezcladas (`Minecraft` como contenedor de rutas y constantes).
- Tests que dependen del entorno real (instalación de Minecraft).
- Falta de `.editorconfig` y `CONTRIBUTING.md` (inconsistencia de estilo).

---

## Qué haces bien
- Estructura MVVM clara (`Views`, `ViewModels`, `Helpers`, `Config`).
- Uso de Serilog para logging estructurado con configuraciones de salida múltiples.
- Implementación práctica de `RelayCommand` y `ViewModelBase`.
- Documentación de visión y features (`README.md`, `features.md`) con riesgos identificados.
- Presencia de tests iniciales — buena intención hacia calidad y CI.

---

## Recomendaciones concretas (pasos siguientes)
1. Habilitar `Nullable` en los proyectos (.NET 8): añadir `<Nullable>enable</Nullable>` en los `.csproj`.
2. Refactorizar `ViewModelBase`:
   - Implementar `protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)`.
3. Mejorar `PreferencesManager`:
   - Validar path con `Path.GetFullPath`, `Directory.Exists`, control de excepciones, y devolver resultado/errores amigables.
4. Mover logs a `LocalApplicationData`:
   - Ejemplo: `%LocalAppData%\NBTMap-Explorer\logs\...`
5. Renombrar variables con typos y aplicar revisión ortográfica en docs.
6. Extraer P/Invoke a `NativeMethods` y documentar contratos de memoria.
7. Tests:
   - Introducir interfaces para detección de instalación (`IMinecraftDetector`) y usar fakes en tests.
   - Evitar asunciones sobre entorno en tests unitarios.
8. Añadir `.editorconfig` y `CONTRIBUTING.md` — tengo un borrador listo para insertar.
9. Integrar análisis estático y pipeline CI (GitHub Actions) que ejecute `dotnet build` y `dotnet test`.

---

## Cambios de ejemplo sugeridos (resumen técnico)
- `ViewModelBase` -> usar `CallerMemberName`.  
- `PreferencesManager` -> validar antes de `Properties.Settings.Default.Save()`.  
- `App.xaml.cs` -> cambiar ruta de log a `Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)` y crear subcarpeta `NBTMap-Explorer\logs`.  
- Tests -> crear `IMinecraftInstallationService` y mockear en tests.

---

¿Deseas que:
- Cree el archivo `project_analysis.md` en el repositorio (puedo crear `CONTRIBUTING.md` y `.editorconfig` también), o
- Genere PRs con los cambios mínimos (OnPropertyChanged, renombrado `isPreseed`, validación básica en `PreferencesManager`, y mover logs)?

Indica una opción y aplico los cambios.