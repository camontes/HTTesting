# Diccionario de datos de la base de datos

> Generado a partir de `src/HR-Platform.Infrastructure/Persistence/Migrations/ApplicationDbContextModelSnapshot.cs`.

## Resumen

- Total de entidades/tablas detectadas: **149**.
- Campos incluidos por tabla: columna, tipo CLR, tipo SQL, nulabilidad, PK/FK e índice.

## Tabla: `ActiveBreaks`

- Entidad: `HR_Platform.Domain.ActiveBreaks.ActiveBreak`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CreationDateFile | DateTime? | timestamp without time zone | Sí | - |  |  |  |
| CreationDateImage | DateTime? | timestamp without time zone | Sí | - |  |  |  |
| Description | string | character varying(2000) | No | 2000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByHR | string | text | No | - |  |  |  |
| File | string | text | No | - |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| Image | string | text | No | - |  |  |  |
| ImageName | string | text | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsPinned | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(200) | No | 200 |  |  |  |
| NameWhoChangedByHR | string | text | No | - |  |  |  |

## Tabla: `Acts`

- Entidad: `HR_Platform.Domain.Acts.Act`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, CompanyId`
- Índices: `CollaboratorId | CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| File | string | text | No | - |  |  |  |
| FileName | string | character varying(100) | No | 100 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |

## Tabla: `Areas`

- Entidad: `HR_Platform.Domain.Areas.Area`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsFormsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `AssignationTypes`

- Entidad: `HR_Platform.Domain.AssignationTypes.AssignationType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `Assignations`

- Entidad: `HR_Platform.Domain.Assignations.Assignation`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsInternalAssignation | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `BankAccounts`

- Entidad: `HR_Platform.Domain.BankAccounts.BankAccount`
- Clave primaria: `Id`
- Campos con FK: `BankId, TypeAccountId`
- Índices: `BankId | TypeAccountId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AccountNumber | string | text | No | - |  |  |  |
| BankId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| TypeAccountId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Banks`

- Entidad: `HR_Platform.Domain.Banks.Bank`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `BenefitClaimAnswers`

- Entidad: `HR_Platform.Domain.BenefitClaimAnswers.BenefitClaimAnswer`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, CompanyId`
- Índices: `CollaboratorId | CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AnotherContraint | string | character varying(50) | No | 50 |  |  |  |
| BenefitName | string | character varying(200) | No | 200 |  |  |  |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DeletedDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Details | string | character varying(500) | No | 500 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoDeletedBenefitClaim | string | character varying(50) | No | 50 |  |  |  |
| EmailWhoManagedClaim | string | character varying(50) | No | 50 |  |  |  |
| HasDeleted | bool | boolean | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsAnotherContraint | bool | boolean | No | - |  |  |  |
| IsAvailableForAll | bool | boolean | No | - |  |  |  |
| IsBenefitAccepeted | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| ManagementDate | DateTime | timestamp without time zone | No | - |  |  |  |
| MinimumMonthsConstraint | int | integer | No | - |  |  |  |
| NameWhoDeletedBenefitClaim | string | character varying(50) | No | 50 |  |  |  |
| NameWhoManagedClaim | string | character varying(50) | No | 50 |  |  |  |
| ReferenceNumber | string | character varying(14) | No | 14 |  |  |  |

## Tabla: `Benefits`

- Entidad: `HR_Platform.Domain.Benefits.Benefit`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AnotherContraint | string | character varying(50) | No | 50 |  |  |  |
| ButtonName | string | text | No | - |  |  |  |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CreationDateFile | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(2000) | No | 2000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| FileURL | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImageName | string | text | No | - |  |  |  |
| ImageURL | string | text | No | - |  |  |  |
| IsAddedButton | bool | boolean | No | - |  |  |  |
| IsAnotherContraint | bool | boolean | No | - |  |  |  |
| IsAvailableForAll | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsPinned | bool | boolean | No | - |  |  |  |
| IsSurveyInclude | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| MinimumMonthsConstraint | int | integer | No | - |  |  |  |
| Name | string | character varying(200) | No | 200 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `BirthdayTemplateSettings`

- Entidad: `HR_Platform.Domain.BirthdayTemplateSettings.BirthdayTemplateSetting`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CustomMessage | string | text | No | - |  |  |  |
| CustomTemplateName | string | text | No | - |  |  |  |
| CustomTemplateURL | string | text | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsAllowSendPost | bool | boolean | No | - |  |  |  |
| IsDefaultTemplate | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `BloodTypes`

- Entidad: `HR_Platform.Domain.BloodTypes.BloodType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `BrigadeAdjustments`

- Entidad: `HR_Platform.Domain.BrigadeAdjustments.BrigadeAdjustment`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| IconId | int | integer | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `BrigadeDocumentations`

- Entidad: `HR_Platform.Domain.BrigadeDocumentations.BrigadeDocumentation`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `BrigadeInventories`

- Entidad: `HR_Platform.Domain.BrigadeInventories.BrigadeInventory`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, UnitMeasureId`
- Índices: `CompanyId | UnitMeasureId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Amount | int | integer | No | - |  |  |  |
| AvailableAmount | int | integer | No | - |  |  |  |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyLocation | string | character varying(200) | No | 200 |  |  |  |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(500) | No | 500 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| ExpirationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsDeleted | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| Observations | string | text | No | - |  |  |  |
| Other | string | character varying(50) | No | 50 |  |  |  |
| PurchaseDate | DateTime | timestamp without time zone | No | - |  |  |  |
| UnitMeasureId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `BrigadeInventoryFiles`

- Entidad: `HR_Platform.Domain.BrigadeInventoryFiles.BrigadeInventoryFile`
- Clave primaria: `Id`
- Campos con FK: `BrigadeInventoryId`
- Índices: `BrigadeInventoryId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| BrigadeInventoryId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FileName | string | character varying(100) | No | 100 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| UrlFile | string | text | No | - |  |  |  |

## Tabla: `BrigadeMembers`

- Entidad: `HR_Platform.Domain.BrigadeMembers.BrigadeMember`
- Clave primaria: `Id`
- Campos con FK: `BrigadeAdjustmentId, CollaboratorId`
- Índices: `BrigadeAdjustmentId | CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| BrigadeAdjustmentId | Guid | uuid | No | - |  | ✅ | ✅ |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| HasBeenDeletedBrigadeAdjustment | bool | boolean | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsBrigadeLeader | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsMainLeader | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |

## Tabla: `Children`

- Entidad: `HR_Platform.Domain.ChildrenNamespace.Children`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Age | int | integer | No | - |  |  |  |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `CoexistenceCommitteeMinutes`

- Entidad: `HR_Platform.Domain.CoexistenceCommitteeMinutes.CoexistenceCommitteeMinute`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `CollaboratorBenefitClaims`

- Entidad: `HR_Platform.Domain.CollaboratorBenefitClaims.CollaboratorBenefitClaim`
- Clave primaria: `Id`
- Campos con FK: `BenefitId, CollaboratorId, CompanyId`
- Índices: `BenefitId | CollaboratorId | CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| BenefitId | Guid | uuid | No | - |  | ✅ | ✅ |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsAccepted | bool | boolean | No | - |  |  |  |
| IsAnySelected | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| ReferenceNumber | string | text | No | - |  |  |  |

## Tabla: `CollaboratorBrigadeInventory`

- Entidad: `HR_Platform.Domain.CollaboratorBrigadeInventories.CollaboratorBrigadeInventory`
- Clave primaria: `Id`
- Campos con FK: `BrigadeInventoryId, CompanyId, UnitMeasureId`
- Índices: `BrigadeInventoryId | CompanyId | UnitMeasureId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| BrigadeInventoryId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DeliveryDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| Observations | string | text | No | - |  |  |  |
| QuantityDelivered | int | integer | No | - |  |  |  |
| ReturnDate | DateTime | timestamp without time zone | No | - |  |  |  |
| SendForAll | bool | boolean | No | - |  |  |  |
| UnitMeasureId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `CollaboratorBrigadeInventoryFile`

- Entidad: `HR_Platform.Domain.CollaboratorBrigadeInventoryFiles.CollaboratorBrigadeInventoryFile`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorBrigadeInventoryId`
- Índices: `CollaboratorBrigadeInventoryId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorBrigadeInventoryId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FileName | string | character varying(100) | No | 100 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| UrlFile | string | text | No | - |  |  |  |

## Tabla: `CollaboratorBrigades`

- Entidad: `HR_Platform.Domain.CollaboratorBrigades.CollaboratorBrigade`
- Clave primaria: `Id`
- Campos con FK: `BrigadeAdjustmentId, CollaboratorBrigadeInventoryId, CollaboratorId`
- Índices: `BrigadeAdjustmentId | CollaboratorBrigadeInventoryId | CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AmountByBrigader | int | integer | No | - |  |  |  |
| BrigadeAdjustmentId | Guid | uuid | No | - |  | ✅ | ✅ |
| CollaboratorBrigadeInventoryId | Guid | uuid | No | - |  | ✅ | ✅ |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `CollaboratorCriteriaAnswer`

- Entidad: `HR_Platform.Domain.CollaboratorCriteriaAnswers.CollaboratorCriteriaAnswer`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorCriteriaId, EvaluationCriteriaTypeId`
- Índices: `CollaboratorCriteriaId | EvaluationCriteriaTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorCriteriaId | Guid | uuid | No | - |  | ✅ | ✅ |
| Comments | string | character varying(2000) | No | 2000 |  |  |  |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CriteriaName | string | character varying(220) | No | 220 |  |  |  |
| CriteriaNameEnglish | string | character varying(220) | No | 220 |  |  |  |
| CriteriaPercentage | int | integer | No | - |  |  |  |
| CriteriaScoreIndexAnswerr | int | integer | No | - |  |  |  |
| CriteriaScoreName | string | character varying(220) | No | 220 |  |  |  |
| CriteriaScoreNameEnglish | string | character varying(220) | No | 220 |  |  |  |
| CriteriaScorePercentage | int | integer | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EvaluationCriteriaTypeId | int | integer | No | - |  | ✅ | ✅ |
| GeneralObjetiveCriteriaPercentage | int | integer | No | - |  |  |  |
| GeneralSubjetiveCriteriaPercentage | int | integer | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsInHistorical | bool | boolean | No | - |  |  |  |
| Position | string | text | No | - |  |  |  |
| PositionEnglish | string | text | No | - |  |  |  |
| PriorityNoveltyId | int | integer | No | - |  |  |  |
| ReferenceNumber | string | character varying(14) | No | 14 |  |  |  |

## Tabla: `CollaboratorCriteria`

- Entidad: `HR_Platform.Domain.CollaboratorCriterias.CollaboratorCriteria`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorEvaluatedId, EvaluatorId, PositionId`
- Índices: `CollaboratorEvaluatedId | EvaluatorId | PositionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorEvaluatedId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EvaluatorId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| PositionId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `CollaboratorDreamMapAnswers`

- Entidad: `HR_Platform.Domain.CollaboratorDreamMapAnswers.CollaboratorDreamMapAnswer`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| SaveCurrent | bool | boolean | No | - |  |  |  |
| TemplateIndicator | int | integer | No | - |  |  |  |

## Tabla: `CollaboratorEducations`

- Entidad: `HR_Platform.Domain.CollaboratorEducations.CollaboratorEducation`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultDaysOfWeekId, DefaultEducationStageId, DefaultMonthId, DefaultProfessionId, DefaultQuestionTypeId, DefaultRepeatEveryEventId, DefaultStudyAreaId, DefaultStudyTypeId, EducationalLevelId`
- Índices: `CollaboratorId | DefaultDaysOfWeekId | DefaultEducationStageId | DefaultMonthId | DefaultProfessionId | DefaultQuestionTypeId | DefaultRepeatEveryEventId | DefaultStudyAreaId | DefaultStudyTypeId | EducationalLevelId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultDaysOfWeekId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultEducationStageId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultMonthId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultProfessionId | int | integer | No | - |  | ✅ | ✅ |
| DefaultQuestionTypeId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultRepeatEveryEventId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultStudyAreaId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultStudyTypeId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EducationalLevelId | Guid? | uuid | Sí | - |  | ✅ | ✅ |
| EducationFileName | string | character varying(100) | No | 100 |  |  |  |
| EducationFileURL | string | character varying(400) | No | 400 |  |  |  |
| EndEducationDate | DateTime? | timestamp without time zone | Sí | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| InstitutionName | string | character varying(50) | No | 50 |  |  |  |
| IsCertificated | bool | boolean | No | - |  |  |  |
| IsCompletedStudy | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| OtherProfessionName | string | character varying(150) | No | 150 |  |  |  |
| StartEducationDate | DateTime? | timestamp without time zone | Sí | - |  |  |  |

## Tabla: `CollaboratorEvents`

- Entidad: `HR_Platform.Domain.CollaboratorEvents.CollaboratorEvent`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, EventId`
- Índices: `CollaboratorId | EventId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EventId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NotificationSent | bool | boolean | No | - |  |  |  |

## Tabla: `CollaboratorGeneralInductions`

- Entidad: `HR_Platform.Domain.CollaboratorGeneralInductions.CollaboratorGeneralInduction`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, InductionId`
- Índices: `CollaboratorId | InductionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| HasInductionBeenDeletedWhenHasCompleted | bool | boolean | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| InductionId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `CollaboratorInductions`

- Entidad: `HR_Platform.Domain.CollaboratorInductions.CollaboratorInduction`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, InductionId`
- Índices: `CollaboratorId | InductionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| InductionId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `CollaboratorLanguages`

- Entidad: `HR_Platform.Domain.CollaboratorLanguages.CollaboratorLanguage`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultLanguageLevelId, DefaultLanguageTypeId`
- Índices: `CollaboratorId | DefaultLanguageLevelId | DefaultLanguageTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultLanguageLevelId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultLanguageTypeId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| OtherLanguageName | string | character varying(50) | No | 50 |  |  |  |
| OtherLanguageNameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `CollaboratorLifePreferences`

- Entidad: `HR_Platform.Domain.CollaboratorLifePreferences.CollaboratorLifePreference`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultLifePreferenceId`
- Índices: `CollaboratorId | DefaultLifePreferenceId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultLifePreferenceId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| OtherLanguageName | string | character varying(50) | No | 50 |  |  |  |
| OtherLanguageNameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `CollaboratorSoftSkills`

- Entidad: `HR_Platform.Domain.CollaboratorSoftSkills.CollaboratorSoftSkill`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultSoftSkillId`
- Índices: `CollaboratorId | DefaultSoftSkillId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultSoftSkillId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| OtherLanguageName | string | character varying(50) | No | 50 |  |  |  |
| OtherLanguageNameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `CollaboratorStates`

- Entidad: `HR_Platform.Domain.CollaboratorStates.CollaboratorState`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `CollaboratorTag`

- Entidad: `HR_Platform.Domain.CollaboratorTags.CollaboratorTag`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, TagId`
- Índices: `CollaboratorId | TagId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| TagId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `CollaboratorTalentPool`

- Entidad: `HR_Platform.Domain.CollaboratorTalentPools.CollaboratorTalentPool`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, TalentPoolId`
- Índices: `CollaboratorId | TalentPoolId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| TalentPoolId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `CollaboratorTechnologyTools`

- Entidad: `HR_Platform.Domain.CollaboratorTechnologyTools.CollaboratorTechnologyTool`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultKnowledgeLevelId, DefaultTechnologyNameId`
- Índices: `CollaboratorId | DefaultKnowledgeLevelId | DefaultTechnologyNameId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultKnowledgeLevelId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultTechnologyNameId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| OtherKnowledgeLevelName | string | character varying(50) | No | 50 |  |  |  |
| OtherKnowledgeLevelNameEnglish | string | character varying(50) | No | 50 |  |  |  |
| OtherTechnologyName | string | character varying(50) | No | 50 |  |  |  |
| OtherTechnologyNameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Collaborators`

- Entidad: `HR_Platform.Domain.Collaborators.Collaborator`
- Clave primaria: `Id`
- Campos con FK: `AssignationId, AssignationTypeId, BankAccountId, BankId, CollaboratorContractId, CollaboratorId, CollaboratorStateId, CompanyId, DocumentTypeId, EconomicLevelId, EducationalLevelId, HealthEntityId, MaritalStatusId, PensionId, PositionId, ProfessionalAdviceId, RoleId, SeveranceBenefitId, TypeAccountId`
- Índices: `AssignationId | AssignationTypeId | BankAccountId | BankId | BusinessEmail | CollaboratorContractId | CollaboratorStateId | CompanyId | DocumentTypeId | EconomicLevelId | EducationalLevelId | HealthEntityId | MaritalStatusId | PensionId | PositionId | ProfessionalAdviceId | RoleId | SeveranceBenefitId | TypeAccountId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AlreadyLogin | bool | boolean | No | - |  |  |  |
| AssignationId | Guid | uuid | No | - |  | ✅ | ✅ |
| AssignationTypeId | int | integer | No | - |  | ✅ | ✅ |
| BankAccountId | Guid | uuid | No | - |  | ✅ | ✅ |
| BankId | Guid | uuid | No | - |  | ✅ | ✅ |
| Birthdate | DateTime? | timestamp without time zone | Sí | - |  |  |  |
| BusinessEmail | string | character varying(100) | No | 100 |  |  | ✅ |
| ChangedBy | string | text | No | - |  |  |  |
| ChildrenNumber | int | integer | No | - |  |  |  |
| City | string | text | No | - |  |  |  |
| CollaboratorContractId | Guid | uuid | No | - |  | ✅ | ✅ |
| CollaboratorStateId | int | integer | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| Country | string | text | No | - |  |  |  |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CVFile | string | text | No | - |  |  |  |
| Department | string | text | No | - |  |  |  |
| Document | string | character varying(100) | No | 100 |  |  |  |
| DocumentTypeId | int | integer | No | - |  | ✅ | ✅ |
| EconomicLevelId | int | integer | No | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EducationalLevelId | Guid | uuid | No | - |  | ✅ | ✅ |
| EmailChangedBy | string | text | No | - |  |  |  |
| EntranceDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FamilyMembersNumber | int | integer | No | - |  |  |  |
| HealthEntityId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsCoexistenceCommitteeMember | bool | boolean | No | - |  |  |  |
| IsCopasstMember | bool | boolean | No | - |  |  |  |
| IsEvaluator | bool | boolean | No | - |  |  |  |
| IsPendingInvitation | bool | boolean | No | - |  |  |  |
| IsSuspended | bool | boolean | No | - |  |  |  |
| LocationAddress | string | character varying(200) | No | 200 |  |  |  |
| LoginCode | string | character varying(10) | No | 10 |  |  |  |
| MaritalStatusId | int | integer | No | - |  | ✅ | ✅ |
| Name | string | character varying(100) | No | 100 |  |  |  |
| OtherDocumentType | string | character varying(50) | No | 50 |  |  |  |
| PensionId | Guid | uuid | No | - |  | ✅ | ✅ |
| PersonalEmail | string | character varying(100) | No | 100 |  |  |  |
| PhoneNumber | string | character varying(50) | No | 50 |  |  |  |
| Photo | string | text | No | - |  |  |  |
| PhotoName | string | text | No | - |  |  |  |
| PositionId | Guid | uuid | No | - |  | ✅ | ✅ |
| PostalCode | string | character varying(50) | No | 50 |  |  |  |
| ProfessionalAdviceId | Guid | uuid | No | - |  | ✅ | ✅ |
| ProfessionalCard | string | text | No | - |  |  |  |
| RecoveryCode | string | character varying(10) | No | 10 |  |  |  |
| RoleId | Guid | uuid | No | - |  | ✅ | ✅ |
| SendNotificationsToPersonalEmail | bool | boolean | No | - |  |  |  |
| SeveranceBenefitId | Guid | uuid | No | - |  | ✅ | ✅ |
| ShowNewFeatures | bool | boolean | No | - |  |  |  |
| SuspensionReason | string | character varying(200) | No | 200 |  |  |  |
| TypeAccountId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Companies`

- Entidad: `HR_Platform.Domain.Companies.Company`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `Email`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Email | string | character varying(100) | No | 100 |  |  | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| Logo | string | text | No | - |  |  |  |
| LogoName | string | character varying(100) | No | 100 |  |  |  |
| MenuName | string | character varying(100) | No | 100 |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| PhoneNumber | string | character varying(50) | No | 50 |  |  |  |
| RequestsEmail | string | character varying(100) | No | 100 |  |  |  |
| URL | string | character varying(250) | No | 250 |  |  |  |

## Tabla: `ContractTypes`

- Entidad: `HR_Platform.Domain.ContractTypes.ContractType`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `CollaboratorContracts`

- Entidad: `HR_Platform.Domain.Contracts.CollaboratorContract`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, ContractTypeId, DefaultContractTypeId, DefaultCurrencyTypeId, DefaultEventTypeId`
- Índices: `CompanyId | ContractTypeId | DefaultContractTypeId | DefaultCurrencyTypeId | DefaultEventTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Arl | string | character varying(50) | No | 50 |  |  |  |
| Bonus | string | character varying(200) | No | 200 |  |  |  |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| ContractTypeId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultContractTypeId | int? | integer | Sí | - |  | ✅ | ✅ |
| DefaultCurrencyTypeId | int | integer | No | - |  | ✅ | ✅ |
| DefaultEventTypeId | int? | integer | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(100) | No | 100 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(100) | No | 100 |  |  |  |
| Salary | string | character varying(20) | No | 20 |  |  |  |

## Tabla: `DefaultArea`

- Entidad: `HR_Platform.Domain.DefaultAreas.DefaultArea`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultAssignations`

- Entidad: `HR_Platform.Domain.DefaultAssignations.DefaultAssignation`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DefaultBanks`

- Entidad: `HR_Platform.Domain.DefaultBanks.DefaultBank`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultBrigadeAdjustments`

- Entidad: `HR_Platform.Domain.DefaultBrigadeAdjustments.DefaultBrigadeAdjustment`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultCollaboratorContracts`

- Entidad: `HR_Platform.Domain.DefaultCollaboratorContracts.DefaultCollaboratorContract`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Arl | string | character varying(50) | No | 50 |  |  |  |
| Bonus | string | character varying(200) | No | 200 |  |  |  |
| ContractType | string | character varying(50) | No | 50 |  |  |  |
| Id | int | integer | No | - | ✅ |  |  |
| Salary | decimal | numeric | No | 10 |  |  |  |

## Tabla: `DefaultContractTypes`

- Entidad: `HR_Platform.Domain.DefaultContractTypes.DefaultContractType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultCurrencyTypes`

- Entidad: `HR_Platform.Domain.DefaultCurrencyTypes.DefaultCurrencyType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultDaysOfWeeks`

- Entidad: `HR_Platform.Domain.DefaultDaysOfWeeks.DefaultDaysOfWeek`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultEducationStages`

- Entidad: `HR_Platform.Domain.DefaultEducationStages.DefaultEducationStage`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultEducationalLevels`

- Entidad: `HR_Platform.Domain.DefaultEducationalLevels.DefaultEducationalLevel`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultEmergencyPlanTypes`

- Entidad: `HR_Platform.Domain.DefaultEmergencyPlanTypes.DefaultEmergencyPlanType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultEvaluationCriteriaScores`

- Entidad: `HR_Platform.Domain.DefaultEvaluationCriteriaScores.DefaultEvaluationCriteriaScore`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| DefaultEvaluationCriteriaId | int | integer | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsForDefaultCriterias | bool | boolean | No | - |  |  |  |
| LowerScore | int | integer | No | - |  |  |  |
| UpperScore | int | integer | No | - |  |  |  |

## Tabla: `DefaultEvaluationCriterias`

- Entidad: `HR_Platform.Domain.DefaultEvaluationCriterias.DefaultEvaluationCriteria`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| EvaluationCriteriaTypeId | int | integer | No | - |  |  |  |
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |
| Percentage | int | integer | No | - |  |  |  |

## Tabla: `DefaultEventReplays`

- Entidad: `HR_Platform.Domain.DefaultEventReplays.DefaultEventReplay`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultEventTypes`

- Entidad: `HR_Platform.Domain.DefaultEventTypes.DefaultEventType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultFamilyCompositions`

- Entidad: `HR_Platform.Domain.DefaultFamilyCompositions.DefaultFamilyComposition`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DefaultFileTypes`

- Entidad: `HR_Platform.Domain.DefaultFileTypes.DefaultFileType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultKnowledgeLevels`

- Entidad: `HR_Platform.Domain.DefaultKnowledgeLevels.DefaultKnowledgeLevel`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultLanguageLevels`

- Entidad: `HR_Platform.Domain.DefaultLanguageLevels.DefaultLanguageLevel`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultLanguageTypes`

- Entidad: `HR_Platform.Domain.DefaultLanguageTypes.DefaultLanguageType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultLifePreferences`

- Entidad: `HR_Platform.Domain.DefaultLifePreferences.DefaultLifePreference`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultMonths`

- Entidad: `HR_Platform.Domain.DefaultMonths.DefaultMonth`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultPensions`

- Entidad: `HR_Platform.Domain.DefaultPensions.DefaultPension`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultPositions`

- Entidad: `HR_Platform.Domain.DefaultPositions.DefaultPosition`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultProfessionalAdvices`

- Entidad: `HR_Platform.Domain.DefaultProfessionalAdvices.DefaultProfessionalAdvice`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameAcronyms | string | character varying(10) | No | 10 |  |  |  |
| NameAcronymsEnglish | string | character varying(10) | No | 10 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DefaultProfessions`

- Entidad: `HR_Platform.Domain.DefaultProfessions.DefaultProfession`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DefaultQuestionTypes`

- Entidad: `HR_Platform.Domain.DefaultQuestionTypes.DefaultQuestionType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultRepeatEveryEvents`

- Entidad: `HR_Platform.Domain.DefaultRepeatEveryEvents.DefaultRepeatEveryEvent`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultRiskTypes`

- Entidad: `HR_Platform.Domain.DefaultRiskTypes.DefaultRiskType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultRoles`

- Entidad: `HR_Platform.Domain.DefaultRoles.DefaultRole`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DefaultSeveranceBenefits`

- Entidad: `HR_Platform.Domain.DefaultSeveranceBenefits.DefaultSeveranceBenefit`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultSoftSkills`

- Entidad: `HR_Platform.Domain.DefaultSoftSkills.DefaultSoftSkill`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultStudyAreas`

- Entidad: `HR_Platform.Domain.DefaultStudyAreas.DefaultStudyArea`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultStudyTypes`

- Entidad: `HR_Platform.Domain.DefaultStudyTypes.DefaultStudyType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultTags`

- Entidad: `HR_Platform.Domain.DefaultTags.DefaultTag`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultTechnologyNames`

- Entidad: `HR_Platform.Domain.DefaultTechnologyNames.DefaultTechnologyName`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultTimeZones`

- Entidad: `HR_Platform.Domain.DefaultTimeZones.DefaultTimeZone`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DefaultTypeAccounts`

- Entidad: `HR_Platform.Domain.DefaultTypeAccounts.DefaultTypeAccount`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DocumentManagementFileTypes`

- Entidad: `HR_Platform.Domain.DocumentManagementFileTypes.DocumentManagementFileType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `DocumentManagement`

- Entidad: `HR_Platform.Domain.DocumentManagements.DocumentManagement`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DocumentManagementFileTypeId`
- Índices: `CollaboratorId | DocumentManagementFileTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DocumentManagementFileTypeId | int | integer | No | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| Other | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `DocumentTypes`

- Entidad: `HR_Platform.Domain.DocumentTypes.DocumentType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `DomainEmails`

- Entidad: `HR_Platform.Domain.DomainEmails.DomainEmail`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Domain | string | character varying(100) | No | 100 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsMainDomainEmail | bool | boolean | No | - |  |  |  |

## Tabla: `DreamMapAnswers`

- Entidad: `HR_Platform.Domain.DreamMapAnswers.DreamMapAnswer`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorDreamMapAnswerId, DreamMapQuestionId`
- Índices: `CollaboratorDreamMapAnswerId | DreamMapQuestionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Answer | string | character varying(500) | No | 500 |  |  |  |
| CollaboratorDreamMapAnswerId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DreamMapQuestionId | Guid? | uuid | Sí | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Question | string | character varying(200) | No | 200 |  |  |  |

## Tabla: `DreamMapQuestions`

- Entidad: `HR_Platform.Domain.DreamMapQuestions.DreamMapQuestion`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Question | string | character varying(200) | No | 200 |  |  |  |

## Tabla: `EconomicLevels`

- Entidad: `HR_Platform.Domain.EconomicLevels.EconomicLevel`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(20) | No | 20 |  |  |  |
| NameEnglish | string | character varying(20) | No | 20 |  |  |  |

## Tabla: `EducationalLevels`

- Entidad: `HR_Platform.Domain.EducationalLevels.EducationalLevel`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `EmergencyContacts`

- Entidad: `HR_Platform.Domain.EmergencyContacts.EmergencyContact`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Address | string | character varying(200) | No | 200 |  |  |  |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| ContactName | string | character varying(100) | No | 100 |  |  |  |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsPrimaryContact | bool | boolean | No | - |  |  |  |
| PhoneNumber | string | character varying(100) | No | 100 |  |  |  |
| Relationship | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `EmergencyPlanTypes`

- Entidad: `HR_Platform.Domain.EmergencyPlanTypes.EmergencyPlanType`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmergencyPlanMainName | string | character varying(50) | No | 50 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `EmergencyPlans`

- Entidad: `HR_Platform.Domain.EmergencyPlans.EmergencyPlan`
- Clave primaria: `Id`
- Campos con FK: `EmergencyPlanTypeId`
- Índices: `EmergencyPlanTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(1000) | No | 1000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmergencyPlanTypeId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImageCreationTime | DateTime | timestamp without time zone | No | - |  |  |  |
| ImageName | string | text | No | - |  |  |  |
| ImageURL | string | text | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| VideoName | string | text | No | - |  |  |  |
| VideoURL | string | text | No | - |  |  |  |

## Tabla: `EvaluationCriteriaScores`

- Entidad: `HR_Platform.Domain.EvaluationCriteriaScores.EvaluationCriteriaScore`
- Clave primaria: `Id`
- Campos con FK: `EvaluationCriteriaId`
- Índices: `EvaluationCriteriaId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EvaluationCriteriaId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IndexScoreAnswer | int | integer | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| LowerScore | int | integer | No | - |  |  |  |
| UpperScore | int | integer | No | - |  |  |  |

## Tabla: `EvaluationCriteriaType`

- Entidad: `HR_Platform.Domain.EvaluationCriteriaTypes.EvaluationCriteriaType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |
| Value | int | integer | No | - |  |  |  |

## Tabla: `EvaluationCriterias`

- Entidad: `HR_Platform.Domain.EvaluationCriterias.EvaluationCriteria`
- Clave primaria: `Id`
- Campos con FK: `EvaluationCriteriaTypeId, PositionId`
- Índices: `EvaluationCriteriaTypeId | PositionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EvaluationCriteriaTypeId | int | integer | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |
| Percentage | int | integer | No | - |  |  |  |
| PositionId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `EventRecurrence`

- Entidad: `HR_Platform.Domain.EventRecurrences.EventRecurrence`
- Clave primaria: `Id`
- Campos con FK: `EventId, EventReplayTypeId`
- Índices: `EventId | EventReplayTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EventId | Guid | uuid | No | - |  | ✅ | ✅ |
| EventReplayTypeId | int | integer | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| Interval | int | integer | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| RecurrenceEndDate | DateTime | timestamp without time zone | No | - |  |  |  |

## Tabla: `EventTypes`

- Entidad: `HR_Platform.Domain.EventTypes.EventType`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Event`

- Entidad: `HR_Platform.Domain.Events.Event`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, EventTypeId, TimeZoneId`
- Índices: `CompanyId | EventTypeId | TimeZoneId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(50) | No | 50 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailCreatedBy | string | character varying(50) | No | 50 |  |  |  |
| EndDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EndTime | TimeSpan | interval | No | - |  |  |  |
| EventTypeId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsDeletedEvent | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| StartDate | DateTime | timestamp without time zone | No | - |  |  |  |
| StartTime | TimeSpan | interval | No | - |  |  |  |
| TimeZoneId | int | integer | No | - |  | ✅ | ✅ |

## Tabla: `EvidenceCoexistenceCommitteeVotes`

- Entidad: `HR_Platform.Domain.EvidenceCoexistenceCommitteeVotes.EvidenceCoexistenceCommitteeVote`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `FamilyCompositions`

- Entidad: `HR_Platform.Domain.FamilyCompositions.FamilyComposition`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `FormAnswerGroupFiles`

- Entidad: `HR_Platform.Domain.FormAnswerGroupFiles.FormAnswerGroupFile`
- Clave primaria: `Id`
- Campos con FK: `FormAnswerGroupId`
- Índices: `FormAnswerGroupId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| File | string | character varying(150) | No | 150 |  |  |  |
| FileName | string | character varying(150) | No | 150 |  |  |  |
| FormAnswerGroupId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `FormAnswerGroupStates`

- Entidad: `HR_Platform.Domain.FormAnswerGroupStates.FormAnswerGroupState`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `FormAnswerGroups`

- Entidad: `HR_Platform.Domain.FormAnswerGroups.FormAnswerGroup`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, FormAnswerGroupStateId, FormId`
- Índices: `CollaboratorId | FormAnswerGroupStateId | FormId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DescriptionState | string | text | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| File | string | text | No | - |  |  |  |
| FileName | string | text | No | - |  |  |  |
| FormAnswerGroupStateId | int | integer | No | - |  | ✅ | ✅ |
| FormId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| ReferenceNumber | string | character varying(18) | No | 18 |  |  |  |

## Tabla: `FormAnswers`

- Entidad: `HR_Platform.Domain.FormAnswers.FormAnswer`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, FormAnswerGroupId, FormQuestionsTypeId`
- Índices: `CollaboratorId | FormAnswerGroupId | FormQuestionsTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Answer | string | character varying(1000) | No | 1000 |  |  |  |
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FormAnswerGroupId | Guid | uuid | No | - |  | ✅ | ✅ |
| FormQuestionsTypeId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| ReferenceNumber | string | character varying(18) | No | 18 |  |  |  |

## Tabla: `FormQuestionsTypes`

- Entidad: `HR_Platform.Domain.FormQuestionsTypes.FormQuestionsType`
- Clave primaria: `Id`
- Campos con FK: `FormId, QuestionTypeId`
- Índices: `FormId | QuestionTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FormId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsRequired | bool | boolean | No | - |  |  |  |
| Question | string | character varying(200) | No | 200 |  |  |  |
| QuestionTypeId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Forms`

- Entidad: `HR_Platform.Domain.Forms.Form`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, NoveltyTypeId`
- Índices: `CompanyId | NoveltyTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NoveltyTypeId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Genders`

- Entidad: `HR_Platform.Domain.Genders.Gender`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `HealthEntities`

- Entidad: `HR_Platform.Domain.HealthEntities.HealthEntity`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, HealthEntityId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `ImprovementPlanResponseFiles`

- Entidad: `HR_Platform.Domain.ImprovementPlanResponseFiles.ImprovementPlanResponseFile`
- Clave primaria: `Id`
- Campos con FK: `ImprovementPlanResponseId`
- Índices: `ImprovementPlanResponseId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChanged | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImprovementPlanResponseId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChanged | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChanged | string | text | No | - |  |  |  |

## Tabla: `ImprovementPlanResponses`

- Entidad: `HR_Platform.Domain.ImprovementPlanResponses.ImprovementPlanResponse`
- Clave primaria: `Id`
- Campos con FK: `ImprovementPlanTaskId`
- Índices: `ImprovementPlanTaskId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImprovementPlanTaskId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| TaskResponse | string | character varying(3000) | No | 3000 |  |  |  |

## Tabla: `ImprovementPlanTaskFiles`

- Entidad: `HR_Platform.Domain.ImprovementPlanTaskFiles.ImprovementPlanTaskFile`
- Clave primaria: `Id`
- Campos con FK: `ImprovementPlanTaskId`
- Índices: `ImprovementPlanTaskId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImprovementPlanTaskId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `ImprovementPlanTasks`

- Entidad: `HR_Platform.Domain.ImprovementPlanTasks.ImprovementPlanTask`
- Clave primaria: `Id`
- Campos con FK: `ImprovementPlanId`
- Índices: `ImprovementPlanId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImprovementPlanId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| TaskDescription | string | character varying(3000) | No | 3000 |  |  |  |

## Tabla: `ImprovementPlans`

- Entidad: `HR_Platform.Domain.ImprovementPlans.ImprovementPlan`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorCriteriaAnswerId`
- Índices: `CollaboratorCriteriaAnswerId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorCriteriaAnswerId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |

## Tabla: `InductionFiles`

- Entidad: `HR_Platform.Domain.InductionFiles.InductionFile`
- Clave primaria: `Id`
- Campos con FK: `InductionId`
- Índices: `InductionId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FileName | string | character varying(50) | No | 50 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| InductionId | Guid | uuid | No | - |  | ✅ | ✅ |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| UrlFile | string | text | No | - |  |  |  |

## Tabla: `Induction`

- Entidad: `HR_Platform.Domain.Inductions.Induction`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AllowForAllCollaborators | bool | boolean | No | - |  |  |  |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DeleteDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(5000) | No | 5000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| EmailWhoDeletedByTH | string | character varying(50) | No | 50 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsInductionDeleted | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| IsVisibleChangeDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `MaritalStatuses`

- Entidad: `HR_Platform.Domain.MaritalStatuses.MaritalStatus`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `MasterUsers`

- Entidad: `HR_Platform.Domain.MasterUsers.MasterUser`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `Email`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Email | string | character varying(100) | No | 100 |  |  | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| LoginCode | string | character varying(10) | No | 10 |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |
| PhoneNumber | string | character varying(50) | No | 50 |  |  |  |
| Photo | string | text | No | - |  |  |  |
| PhotoName | string | text | No | - |  |  |  |
| RoleName | string | character varying(100) | No | 100 |  |  |  |
| RoleNameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `Minutes`

- Entidad: `HR_Platform.Domain.Minutes.Minute`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `NewCommunications`

- Entidad: `HR_Platform.Domain.NewCommunications.NewCommunication`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CreationDateFile | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(2000) | No | 2000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| FileURL | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImageName | string | text | No | - |  |  |  |
| ImageURL | string | text | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsSurveyInclude | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(200) | No | 200 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `NoteFiles`

- Entidad: `HR_Platform.Domain.NoteFiles.NoteFile`
- Clave primaria: `Id`
- Campos con FK: `NoteId`
- Índices: `NoteId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FileName | string | character varying(50) | No | 50 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NoteId | Guid | uuid | No | - |  | ✅ | ✅ |
| UrlFile | string | text | No | - |  |  |  |

## Tabla: `NoteViewers`

- Entidad: `HR_Platform.Domain.NoteViewers.NoteViewer`
- Clave primaria: `Id`
- Campos con FK: `NoteId, ViewerId`
- Índices: `NoteId | ViewerId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NoteId | Guid | uuid | No | - |  | ✅ | ✅ |
| ViewerId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Notes`

- Entidad: `HR_Platform.Domain.Notes.Note`
- Clave primaria: `Id`
- Campos con FK: `AssignedTo, CreatedBy, ParentNoteId`
- Índices: `AssignedTo | CreatedBy | ParentNoteId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AssignedTo | Guid | uuid | No | - |  | ✅ | ✅ |
| CreatedBy | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(10000) | No | 10000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsPublic | bool | boolean | No | - |  |  |  |
| ParentNoteId | Guid? | uuid | Sí | - |  | ✅ | ✅ |

## Tabla: `NotificationNotes`

- Entidad: `HR_Platform.Domain.NotificationNotes.NotificationNote`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsRead | bool | boolean | No | - |  |  |  |

## Tabla: `NotificationTypes`

- Entidad: `HR_Platform.Domain.NotificationTypes.NotificationType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Message | string | character varying(200) | No | 200 |  |  |  |
| MessageEnglish | string | character varying(200) | No | 200 |  |  |  |

## Tabla: `Notifications`

- Entidad: `HR_Platform.Domain.Notifications.Notification`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, NotificationTypeId`
- Índices: `CollaboratorId | NotificationTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsRead | bool | boolean | No | - |  |  |  |
| Message | string | character varying(1000) | No | 1000 |  |  |  |
| MessageEnglish | string | character varying(1000) | No | 1000 |  |  |  |
| NotificationTypeId | int | integer | No | - |  | ✅ | ✅ |
| SourceEmail | string | character varying(50) | No | 50 |  |  |  |
| SourceInitials | string | character varying(5) | No | 5 |  |  |  |
| SourceName | string | character varying(50) | No | 50 |  |  |  |
| SourcePhoto | string | character varying(150) | No | 150 |  |  |  |

## Tabla: `OccupationalRecommendations`

- Entidad: `HR_Platform.Domain.OccupationalRecommendations.OccupationalRecommendation`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `OccupationalTests`

- Entidad: `HR_Platform.Domain.OccupationalTests.OccupationalTest`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId, DefaultFileTypeId`
- Índices: `CollaboratorId | DefaultFileTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| DefaultFileTypeId | int | integer | No | - |  | ✅ | ✅ |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| Other | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `OrganizationCharts`

- Entidad: `HR_Platform.Domain.OrganizationCharts.OrganizationChart`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileCreatedDate | DateTime | timestamp without time zone | No | - |  |  |  |
| FileName | string | text | No | - |  |  |  |
| FileURL | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsByFile | bool | boolean | No | - |  |  |  |
| IsByUrl | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Pensions`

- Entidad: `HR_Platform.Domain.Pensions.Pension`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `PermissionGroups`

- Entidad: `HR_Platform.Domain.PermissionGroups.PermissionGroup`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `Permissions`

- Entidad: `HR_Platform.Domain.Permissions.Permission`
- Clave primaria: `Id`
- Campos con FK: `PermissionGroupId`
- Índices: `PermissionGroupId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Description | string | character varying(150) | No | 150 |  |  |  |
| DescriptionEnglish | string | character varying(150) | No | 150 |  |  |  |
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |
| PermissionGroupId | int | integer | No | - |  | ✅ | ✅ |
| ValidationString | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Positions`

- Entidad: `HR_Platform.Domain.Positions.Position`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| CriteriasEditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| DescriptionEnglish | string | character varying(200) | No | 200 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| ObjectiveCriteria | int | integer | No | - |  |  |  |
| PositionFile | string | text | No | - |  |  |  |
| PositionFileName | string | text | No | - |  |  |  |
| SubjectiveCriteria | int | integer | No | - |  |  |  |

## Tabla: `PriorityNovelties`

- Entidad: `HR_Platform.Domain.PriorityNovelties.PriorityNovelty`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `ProfessionalAdvices`

- Entidad: `HR_Platform.Domain.ProfessionalAdvices.ProfessionalAdvice`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameAcronyms | string | character varying(10) | No | 10 |  |  |  |
| NameAcronymsEnglish | string | character varying(10) | No | 10 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `QuestionTypes`

- Entidad: `HR_Platform.Domain.QuestionTypes.QuestionType`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Regulations`

- Entidad: `HR_Platform.Domain.Regulations.Regulation`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | text | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `RiskTypeMains`

- Entidad: `HR_Platform.Domain.RiskTypeMains.RiskTypeMain`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, EmergencyPlanTypeId`
- Índices: `CompanyId | EmergencyPlanTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmergencyPlanTypeId | Guid | uuid | No | - |  | ✅ | ✅ |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `Risks`

- Entidad: `HR_Platform.Domain.Risks.Risk`
- Clave primaria: `Id`
- Campos con FK: `RiskTypeMainId`
- Índices: `RiskTypeMainId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(1000) | No | 1000 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| ImageCreationTime | DateTime | timestamp without time zone | No | - |  |  |  |
| ImageName | string | text | No | - |  |  |  |
| ImageURL | string | text | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| RiskTypeMainId | Guid | uuid | No | - |  | ✅ | ✅ |
| VideoName | string | text | No | - |  |  |  |
| VideoURL | string | text | No | - |  |  |  |

## Tabla: `Roles`

- Entidad: `HR_Platform.Domain.Roles.Role`
- Clave primaria: `Id`
- Campos con FK: `AreaId, CompanyId`
- Índices: `AreaId | CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| AreaId | Guid | uuid | No | - |  | ✅ | ✅ |
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameEnglish | string | character varying(100) | No | 100 |  |  |  |

## Tabla: `RolesPermission`

- Entidad: `HR_Platform.Domain.RolesPermissions.RolePermission`
- Clave primaria: `Id`
- Campos con FK: `PermissionId, RoleId`
- Índices: `PermissionId | RoleId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsCheck | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| PermissionId | int | integer | No | - |  | ✅ | ✅ |
| RoleId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `SeveranceBenefits`

- Entidad: `HR_Platform.Domain.SeveranceBenefits.SeveranceBenefit`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `SurveyQuestionMandatoryTypes`

- Entidad: `HR_Platform.Domain.SurveyQuestionMandatoryTypes.SurveyQuestionMandatoryType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `SurveyQuestionTypes`

- Entidad: `HR_Platform.Domain.SurveyQuestionTypes.SurveyQuestionType`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| Id | int | integer | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `SurveyQuestions`

- Entidad: `HR_Platform.Domain.SurveyQuestions.SurveyQuestion`
- Clave primaria: `Id`
- Campos con FK: `SurveyId, SurveyQuestionMandatoryTypeId, SurveyQuestionTypeId`
- Índices: `SurveyId | SurveyQuestionMandatoryTypeId | SurveyQuestionTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| SurveyId | Guid | uuid | No | - |  | ✅ | ✅ |
| SurveyQuestionMandatoryTypeId | int | integer | No | - |  | ✅ | ✅ |
| SurveyQuestionTypeId | int | integer | No | - |  | ✅ | ✅ |
| Text | string | character varying(200) | No | 200 |  |  |  |

## Tabla: `Surveys`

- Entidad: `HR_Platform.Domain.Surveys.Survey`
- Clave primaria: `Id`
- Campos con FK: `CompanyId, SurveyTypeId`
- Índices: `CompanyId | SurveyTypeId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(200) | No | 200 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(100) | No | 100 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| IsVisible | bool | boolean | No | - |  |  |  |
| Name | string | character varying(100) | No | 100 |  |  |  |
| NameWhoChangedByTH | string | character varying(100) | No | 100 |  |  |  |
| SurveyTypeId | Guid | uuid | No | - |  | ✅ | ✅ |

## Tabla: `Tag`

- Entidad: `HR_Platform.Domain.Tags.Tag`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `TalentPool`

- Entidad: `HR_Platform.Domain.TalentPools.TalentPool`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Description | string | character varying(500) | No | 500 |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsArchived | bool | boolean | No | - |  |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Tittle | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `TypeAccounts`

- Entidad: `HR_Platform.Domain.TypeAccounts.TypeAccount`
- Clave primaria: `Id`
- Campos con FK: `CompanyId`
- Índices: `CompanyId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CompanyId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `UnitMeasures`

- Entidad: `HR_Platform.Domain.UnitMeasures.UnitMeasure`
- Clave primaria: `Id`
- Campos con FK: `(no detectados)`
- Índices: `(no detectados)`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| Name | string | character varying(50) | No | 50 |  |  |  |
| NameEnglish | string | character varying(50) | No | 50 |  |  |  |

## Tabla: `WorkplaceEvidences`

- Entidad: `HR_Platform.Domain.WorkplaceEvidences.WorkplaceEvidence`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `WorkplaceInformations`

- Entidad: `HR_Platform.Domain.WorkplaceInformations.WorkplaceInformation`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |

## Tabla: `WorkplaceRecommendations`

- Entidad: `HR_Platform.Domain.WorkplaceRecommendations.WorkplaceRecommendation`
- Clave primaria: `Id`
- Campos con FK: `CollaboratorId`
- Índices: `CollaboratorId`

| Columna | Tipo CLR | Tipo SQL | Nulo | MaxLength | PK | FK | Index |
|---|---|---|---|---:|:---:|:---:|:---:|
| CollaboratorId | Guid | uuid | No | - |  | ✅ | ✅ |
| CreationDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EditionDate | DateTime | timestamp without time zone | No | - |  |  |  |
| EmailWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| FileName | string | character varying(200) | No | 200 |  |  |  |
| Id | Guid | uuid | No | - | ✅ |  |  |
| IsDeleteable | bool | boolean | No | - |  |  |  |
| IsEditable | bool | boolean | No | - |  |  |  |
| NameWhoChangedByTH | string | character varying(50) | No | 50 |  |  |  |
| UrlFile | string | text | No | - |  |  |  |
| UrlPhotoWhoChangedByTH | string | text | No | - |  |  |  |
