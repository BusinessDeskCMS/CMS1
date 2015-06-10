/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.resize_enabled = false;   
    config.htmlEncodeOutput = false;
    config.basicEntities = false;
    config.entities = false;
    config.entities_greek = false;
    config.entities_latin = false;
    config.entities_processNumerical = false;
    config.extraPlugins = 'autosave';
    config.extraPlugins = 'wordcount';
    config.wordcount = {

        // Whether or not you want to show the Word Count
        showWordCount: true,

        // Whether or not you want to show the Char Count
        showCharCount: true,

        // Whether or not to include Html chars in the Char Count
        countHTML: false,

        // Option to limit the characters in the Editor
        charLimit: 'unlimited',

        // Option to limit the words in the Editor
        wordLimit: 'unlimited'
    };
};
