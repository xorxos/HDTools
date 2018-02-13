﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Redesign
{

    /// <summary>
    /// Data for a <see cref="PhraseListItemViewModel"/>
    /// </summary>
    class PhraseListDataModel : PhraseListViewModel
    {
        private static PhraseListItemViewModel _selectedItem;
        public PhraseListItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(_selectedItem)); }
        }

        /// <summary>
        /// A single instance of the data model
        /// </summary>
        #region Singleton
        private static PhraseListDataModel _instance;
        public static PhraseListDataModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhraseListDataModel();
                }
                return _instance;
            }
        }
        #endregion

        #region Contructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public PhraseListDataModel()
        {
            Items = new List<PhraseListItemViewModel>
            {
                new PhraseListItemViewModel
                {
                    Abbreviation = "#IFSACC",
                    Description = "How to request access to IFS and the link to form",
                    IsSelected = true
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#ADPLink",
                    Description = "Link to the training video for ADP"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#ADPRegister",
                    Description = "Verbiage instructing user to watch ADP training video"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#CompletedAcc",
                    Description = "Verbiage after submitting access request on behalf of user"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#ComputerName",
                    Description = "Verbiage instructing user on how to find computer name"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#ConferenceAcc",
                    Description = "Access request verbiage on how to request conference room/resource"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#Confluence",
                    Description = "Steps to request access to Confluence"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#CRSAccess",
                    Description = "Steps to request access to CRS"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#Distro",
                    Description = "Steps to request a change/modification to a distro list"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#DriveAcc",
                    Description = "Steps to request access to a drive/folder/file"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#EmailPassword",
                    Description = "Verbiage to send when a password ticket is submitted via email"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#GenAcc",
                    Description = "General steps on how to request access"
                },
                new PhraseListItemViewModel
                {
                    Abbreviation = "#RemoteDesktop",
                    Description = "Steps on how to use remote desktop"
                }
            };

            //Set the initial selected item object. Run SetItemTrue twice to keep the top item at the top.
            var item = Items[0];
            SetItemTrue(item.Abbreviation);
            SetItemTrue(item.Abbreviation);
        }
        #endregion

        

        public void SetItemTrue(string Abbreviation)
        {
            if (Items.Count != 0)
            {
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (Items[i].Abbreviation.Equals(Abbreviation, StringComparison.Ordinal))
                    {
                        Console.Write("Item found: " + Items[i].Abbreviation + "!     " + Items[i].IsSelected.ToString());
                        PhraseListItemViewModel newItem = Items[i];
                        newItem.IsSelected = true;
                        Items.Remove(Items[i]);
                        Items.Add(newItem);
                        Console.WriteLine(" ->     " + newItem.IsSelected.ToString());
                        OnPropertyChanged(nameof(Items));

                        SelectedItem = newItem;
                        OnPropertyChanged(nameof(SelectedItem));
                    }
                    else
                    {
                        PhraseListItemViewModel newItem = Items[i];
                        newItem.IsSelected = false;
                        Items.Remove(Items[i]);
                        Items.Add(newItem);
                        Console.WriteLine(" ->     " + newItem.IsSelected.ToString());
                    }
                }


                Console.WriteLine();
                Console.WriteLine();
                foreach (var item in Items)
                {
                    Console.WriteLine("------- START -------");
                    Console.WriteLine("Phrase: " + item.Abbreviation);
                    Console.WriteLine("Description: " + item.Description);
                    Console.WriteLine("Content: " + item.Content);
                    Console.WriteLine("-------  END  -------");
                }
            }
        }
    }
}
