mergeInto(LibraryManager.library, {
    UserDisplayLoaded: function ()
    {
        window.UserDisplayLoaded();
    },

    ReceiveUserData: function (userData)
    {
        window.ReceiveLeaderBoardData(userData);
    }
});