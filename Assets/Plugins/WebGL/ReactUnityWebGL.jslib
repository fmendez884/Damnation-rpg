mergeInto(LibraryManager.library, {
    UserDisplayLoaded: function ()
    {
        window.UserDisplayLoaded();
    },

    ReceiveLeaderBoardData: function (leaderBoardData)
    {
        window.ReceiveLeaderBoardData(Pointer_stringify(leaderBoardData));
    },
});