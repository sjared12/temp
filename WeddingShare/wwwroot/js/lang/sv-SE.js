﻿﻿class Localization extends BaseLocalization {
    constructor() {
        super('sv-SE', {
            "Loading": "Laddar...",
            "Or": "Eller",
            "Code": "kod",
            "Next": "Nästa",
            "Review": "Recension",
            "Approve": "Godkänn",
            "Reject": "Avvisa",
            "Create": "Skapa",
            "Import": "Importera",
            "Export": "Exportera",
            "Wipe": "Sudda",
            "Remove": "Ta Bort",
            "Reset": "Återställ",
            "Upload": "Ladda upp",
            "Download": "Ladda ner",
            "Update": "Updatera",
            "Cancel": "Avbryt",
            "Close": "Stäng",
            "Delete": "Ta bort",
            "Error": "Fel",
            "Errors": "Fel",
            "Validate": "Validera",
            "Login": "Logga in",
            "Login_Invalid_Username": "Vänligen ange ett giltigt användarnamn",
            "Login_Invalid_Password": "Vänligen ange ett giltigt lösenord",
            "Login_Invalid_Details": "Ogiltigt användarnamn eller lösenord har angetts",
            "Login_Failed": "Inloggning misslyckades",
            "Identity_Check": "Identitetskontroll",
            "Identity_Check_Name": "Namn",
            "Identity_Check_Hint": "Berätta vem du är så att vi vet vem som laddat upp minnen."
            "Identity_Check_Placeholder": "T.ex. Jane Doe, Jimmy, farbror Bob",
            "Identity_Check_Tell_Us": "Berätta för oss",
            "Identity_Check_Stay_Anonymous": "Var anonym",
            "Identity_Check_Change_Identity": "Ändra identitet",
            "Identity_Check_Change": "Ändra",
            "Identity_Check_Invalid_Name": "Ogiltigt namn",
            "Identity_Check_Invalid_Name_Msg": "Vänligen ange ett giltigt namn",
            "Identity_Check_Set_Failed": "Det gick inte att ange användaridentitet",
            "Gallery": "Galleri",
            "Gallery_Name": "Gallerinamn",
            "Gallery_Name_Hint": "Vänligen ange ett nytt gallerinamn",
            "Gallery_Secret_Key": "Hemlig nyckel",
            "Gallery_Secret_Key_Hint": "Vänligen ange en ny hemlig nyckel",
            "Gallery_Invalid_Name": "Vänligen ange ett giltigt gallerinamn",
            "Gallery_Invalid_Secret_Key": "Ogiltig hemlig nyckel, försök igen",
            "Gallery_Missing_Id": "Galleri-ID kan inte vara tomt",
            "Gallery_Missing_Name": "Gallerinamnet får inte vara tomt",
            "Gallery_Create": "Skapa galleri",
            "Gallery_Create_Success": "Galleri skapat framgångsrikt",
            "Gallery_Create_Failed": "Det gick inte att skapa galleri",
            "Gallery_Edit": "Redigera galleri",
            "Gallery_Edit_Success": "Uppdaterat galleri",
            "Gallery_Edit_Failed": "Det gick inte att uppdatera galleriet",
            "Gallery_Wipe": "Rensa galleri",
            "Gallery_Wipe_Message": "Är du säker på att du vill radera galleriet '{name}'?",
            "Gallery_Wipe_Success": "Rensat galleri",
            "Gallery_Wipe_Failed": "Det gick inte att rensa galleriet",
            "Gallery_Delete": "Ta bort galleri",
            "Gallery_Delete_Message": "Är du säker på att du vill ta bort galleriet '{name}'?",
            "Gallery_Delete_Success": "Galleriet har tagits bort",
            "Gallery_Delete_Failed": "Det gick inte att ta bort galleriet",
            "Upload_Progress": "Laddar upp objekt {index} av {count}...",
            "Upload_Success": "Uppladdat {count} objekt(er)",
            "Upload_Success_Pending_Review": "Uppladdat {count} objekt(er) som väntar på granskning",
            "Upload_No_Files_Detected": "Inga filer upptäcktes att ladda upp",
            "Upload_Invalid_Gallery_Detected": "Ogiltigt galleri-id upptäckt",
            "Upload_Invalid_Upload_Url": "Kunde inte hitta uppladdningsadress",
            "Review_Id_Missing": "Kunde inte hitta artikel-ID",
            "Review_Failed": "Det gick inte att granska objektet",
            "Bulk_Review": "Massgranskning",
            "Bulk_Review_Approve_Failed": "Det gick inte att godkänna alla objekt",
            "Bulk_Review_Reject_Failed": "Det gick inte att avvisa alla objekt",
            "Bulk_Review_Message": "Vill du godkänna eller avvisa alla väntande objekt?",
            "Delete_Item": "Ta bort objekt",
            "Delete_Item_Success": "Borttaget objekt",
            "Delete_Item_Failed": "Det gick inte att ta bort objekt",
            "Delete_Item_Message": "Är du säker på att du vill ta bort '{name}'?",
            "Delete_Item_Id_Missing": "Id kan inte vara tomt",
            "Import_Data": "Importera data",
            "Import_Data_Success": "Lyckades importerad data",
            "Import_Data_Failed": "Det gick inte att importera data",
            "Import_Data_Backup_File": "Säkerhetskopieringsfil",
            "Import_Data_Backup_Hint": "Välj ett WeddingShare backup-arkiv",
            "Import_Data_Select_File": "Välj en importfil",
            "Export_Data": "Exportera data",
            "Export_Data_Success": "Exporterade data framgångsrikt",
            "Export_Data_Failed": "Det gick inte att exportera data",
            "Export_Data_Message": "Är du säker på att du vill fortsätta?",
            "Wipe_Data": "Radera data",
            "Wipe_Data_Message": "Är du säker på att du vill radera all data?",
            "Wipe_Data_Success": "Raderat data framgångsrikt",
            "Wipe_Data_Failed": "Det gick inte att radera data",
            "Download_Failed": "Det gick inte att ladda ner galleriet",
            "Browser_Does_Not_Support": "Din webbläsare stöder inte den här funktionen",
            "2FA_Setup": "2FA Setup",
            "2FA_Scan_With_App": "Skanna följande bild med din app",
            "2FA_Manually_Enter_Code": "Ange koden manuellt:",
            "2FA_Code_Hint": "Vänligen ange koden som genereras av din 2FA-app",
            "2FA_Set_Successfully": "Lyckades konfigurera 2FA",
            "2FA_Set_Failed": "Det gick inte att konfigurera 2FA",
			"2FA_Reset_Successfully": "Återställ 2FA framgångsrikt",
			"2FA_Reset_Failed": "Det gick inte att återställa 2FA"
        });
    }
}

const localization = new Localization();