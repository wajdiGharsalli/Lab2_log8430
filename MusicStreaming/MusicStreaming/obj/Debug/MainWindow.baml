   M S B A M L   `   `   `  ����I  EMusicStreaming, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MusicStreamingMusicStreaming  5   	       UserInterface.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:MusicStreaming     5   	   -   5          � Title$  Music Streaming��5   	   $	��350��6!   $	��525��6.   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   ]� $��0��6   $��0��6$   $��2��61   
�������.+�5      �� -   5        $��m_searchButton��5      $	��Top=�6+   $��0q�6@   $
��20,5q�6T   $��Search��6   �� 5      -   5        $��m_playlistsButton��5      $	��Top=�6.   $��0q�6C   $
��20,5q�6W   $	��Top=�6�   .��6�   	PlayLists6�   �� 5      -   5        $��Bottom=�5      $��Bottom=�6-   $��0q�6H   $
��20,5q�6\   
���/���.��6�   Connect Spotify6�   5      �� 5   
   $��1��6   $��0��6'   $
��Auto=�65   .��5   
   �� 
����ح� �� CanVerticallyScroll .  �� CanHorizontallyScroll . .+�5      ]� -   5        $��m_createPlaylistPanel��5      $��
10,10,10,2q�65   $��Stretch=�6I   .+�5      �� -   5        $��20, 5q�5      $��Right=�6)   .��6^   Create5      �� 5      -   5        $��m_playListTextBox��5      $��0,0,5,0q�63   $
��left=�6D   5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5       �� -   5        $��m_playlistControl��5    3   $��
10,10,10,2q�6   
���������6L   �� *	Playlists+��5!      �� 5"      .R�5#      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5$   "   �� -	   5        $��20��5$   @   $��20��6L   .��5%   &   �� $��./Icons/play.png��6,    � ToolTip$ Play playlist��6F   5&   #   �� 5'   "   $��5,0,0,0q�6,   ��6>   �� 5(      5)      5+      &   System.Windows.Controls.DataGrid  5,      -
   5        $��m_playlistDataGrid��5,      $��10, 0q�69      AutoGenerateColumns .     
IsReadOnly . 	   	RowHeight$	 25Q�6�   $��Center=�6�   ��6H   �� *CurrentPlaylist+
   Columns
 5-      4  .System.Windows.Controls.DataGridTemplateColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��65      CellTemplate 5/   "   �� 50   &   .R�51   *   �� $ 
Horizontal=�65   $��Center=�6N   .+�52   .   �� -   5        .��53   2   �� $��./Icons/play.png��68   54   /   �� 55   .    �� Padding$ 5,0,5,0q�68   ��6J   �� *Name+56   +   57   '   59      0  *System.Windows.Controls.DataGridTextColumn  5:      $ Album��6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Album+6K     5;      $ Artist��6L    61   �� *Artist+6L     5<      $ Duration (Min)��6Q    61   �� *DurationMin+6Q   5>      5?      ]� 5@      -   5        $��m_searchPanel��5@      $��
10,10,10,2q�6-   $��Stretch=�6A   .+�5A      �� -   5        $��20, 5q�5A      $��Right=�6)   .��6V   Search5B      �� 5C      -   5        $��m_searchTextBox��5C      $��0,0,5,0q�61   $
��left=�6B   5D      �� 5E      -   5        $��m_systemsPanel��5E      $ 
Horizontal=�6/   $��
10,0,10,10q�6H   .+�5F      �� -   5        $��m_spotifySearchCheck��5F      $��Spotify��68   $��0,0,10,0q�6J   �� 5G      -   5        $��m_deezerSearchCheck��5G      $��Deezer��67   $��0,0,10,0q�6I   �� 5H      -   5        $��m_jamendoSearchCheck��5H      $��Jamendo��68   5I        5J      -   5        $��m_foundDataGrid��5J      $��10, 0q�6.    .   . $	 25Q�6�   $��Center=�6�   ��6=   �� *FoundTracks+
 5K        5L      $
 Name��6-    5M      �� 5N      .R�5O   "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5P   &   �� -   5        .��5Q   *   �� $��./Icons/play.png��60   5R   '   �� 5S   &   $ 5,0,5,0q�60   $	��200��6X   ��6B   �� *Name+6X   5T   #   5U      5W        5X      $ Album��6G      MaxWidth$	 200Q�6V    6-   �� *Album+6V     5Y      $ Artist��6H   $ 60Q�6X    6-   �� *Artist+6X     5Z      $ Duration��6I    6)   �� *DurationMin+6I     5[      $ System��6B    6)   �� *Type+6B   5]      5^      5_      ]� 5`   
   $��1��6   $��1��6$   $	��120��61   
�������.+�5a      �� -   5        $��m_trackImage��5a      $��Right=�6(   $	��120��6?   $��Right=�6K   �� 5b      
���P��.+�5c      ]� $��	10,5,10,0q�6   .+�5d      �� $
��Left=�6!   $
��Left=�67   .+�5e      �� -   5        $��m_titleTrackTextBox��5e   $   �� 5f      -   5        $��m_albumTrackTextBox��5f   $   �� 5g      -   5        $��m_artistTrackTextBox��5g   $   5h      �� 5i      $ 
Horizontal=�6!   $��Right=�6:   $	��Top=�6V   .+�5j      �� -   5        $��m_playlistsComboBox��5j   #   $��0,0,5,0q�6>   $	��100��6O   ��6[   �� *	Playlists+�� 5k      -   5        $��m_addToPlaylistButton��5k   !   $	��5,2q�6>   $��Add to playlist��6L   6f   5l      5m      �� 5n      $��Right=�6   5p      �� 5q      $��Bottom=�6   $��Bottom=�65   .+�5r      �� -   5        $��m_trackSlider��5r      $
��10,0q�62   $��40��6@    �� TickPlacement$ BottomRight=�6L    �� TickFrequency$ 10Q�6h   �� 5s      $ 
Horizontal=�6!   $��Center=�6:   .+�5t      �� -   5        $��0,0,5,0q�5t   !   $��Previous��62   6E   �� 5u      -   5        $��0,0,5,0q�5u   !   $��
Play/Pause��62   6G   �� 5v      -   5        $��0,0,5,0q�5v   !   $
��Next��62   6A   5w      5x      5y      5z      5{      5|      