$(document).ready(async () => {
    async function getRecords(page,athleteID,eventID,meetID) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: "/Record/GetRecords?page=1" + (athleteID != null ? "&athleteID=" + athleteID : "") + (eventID != null ? "&eventID=" + eventID : "") + (meetID != null ? "&meetID=" + meetID : ""),
                dataType: "json",
                success: (data) => {
                    resolve(data);
                },
                error: (jqXHR, textStatus) => {
                    reject(textStatus);
                }
            });
        });
    }

    var recordData = await getRecords(1);

    console.log(recordData);

    var athleteData = recordData.reduce((acc, item) => {
        if (Array.isArray(acc)) {
            return acc.some(a => a.AthleteID == item.AthleteID) ? acc : [...acc, item];
        }
        return [acc];
    });

    $("#athleteSearchInput").autocomplete({
        source: athleteData.map(a => a.Name),
        select: function (e, ui) {
            $("#athleteSearchInput").val(ui.item.label);
            $("#athleteFilter").submit();
        }
    });

    $("#athleteFilter").submit((e) => {
        e.preventDefault();
        let nameInput = $("#athleteSearchInput").val();
        var athlete = athleteData.find(a => a.Name == nameInput);
        if (athlete == undefined) {
            console.log("NO ATHLETE FOUND");
            $(".result").show();
            return false;
        }
        console.log(athlete);
        $(".result").each((i, row) => {
            console.log(row)
            if (parseInt($(row).attr("data-athlete")) == athlete.AthleteID) {
                $(row).show();
            }
            else {
                $(row).hide();
            }
        });
    });
});